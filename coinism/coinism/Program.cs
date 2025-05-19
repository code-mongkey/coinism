using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using coinism.Core.Interfaces;
using coinism.Core.Models;
using coinism.Data;
using coinism.Engines;
using coinism.Notifier;
using coinism.Strategies;
using coinism.Upbit; // 전략 클래스들이 여기에 포함된다고 가정

namespace coinism
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            var configPath = Path.Combine(AppContext.BaseDirectory, "Config", "settings.json");
            if (!File.Exists(configPath))
            {
                Console.WriteLine("[ERROR] 설정 파일이 없습니다.");
                return;
            }

            var settings = JsonSerializer.Deserialize<Settings>(File.ReadAllText(configPath));

            string dbPath = Path.Combine(AppContext.BaseDirectory, "coinism.db");
            DbInitializer.EnsureDatabase(dbPath);
            using var context = new SqliteContext(dbPath);

            // ✅ 정적으로 전략을 직접 등록
            List<IStrategy<Candle, TradeAction, StrategyResult>> strategies = new()
            {
                new SmaCrossStrategy(),
                new BollingerBandStrategy(),
                new SmaBollingerCombo()
            };

            // UpbitClient 초기화
            var upbitClient = new UpbitClient(settings.ApiKey, settings.ApiSecret);
            string market = "KRW-BTC";
            string interval = "minutes/15";

            foreach (var strategy in strategies)
            {
                strategy.Init(settings);
                string strategyName = strategy.GetType().Name;

                IEngine engine = settings.RunMode == "Backtest"
                    ? new BacktestEngine(
                        new CandleRepository(context).GetCandles("KRW-BTC", "15m", DateTime.UtcNow.AddDays(-7)),
                        strategy)
                    : new LiveTradingEngine(
                        strategy,
                        async () =>
                        {
                            try
                            {
                                var candles = await upbitClient.GetCandlesAsync(market, interval, 1);
                                var c = candles?[0];
                                return new Candle
                                {
                                    Timestamp = c.CandleTimeKst,
                                    Open = c.OpeningPrice,
                                    High = c.HighPrice,
                                    Low = c.LowPrice,
                                    Close = c.TradePrice,
                                    Volume = c.Volume,
                                    Market = c.Market,
                                    Interval = interval
                                };
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"[ERROR] 캔들 불러오기 실패: {ex.Message}");
                                return null;
                            }
                        },
                        async (action) =>
                        {
                            if (action.Type == TradeType.None)
                                return;

                            Console.WriteLine($"[실행됨] {action.Type} @ {action.Price} ({action.Reason})");

                            new TradeLogRepository(context).InsertTrade(action, strategyName);
                            await SlackNotifier.NotifyAsync(settings.SlackUrl, $"[{strategyName}] {action.Type} @ {action.Price} - {action.Reason}");
                        },
                        settings.IntervalSeconds
                    );

                Console.WriteLine($"[{strategyName}] 전략 실행 시작");
                using var cts = new CancellationTokenSource();
                Console.CancelKeyPress += (s, e) =>
                {
                    Console.WriteLine("[중단 요청]");
                    cts.Cancel();
                    e.Cancel = true;
                };

                await engine.RunAsync(cts.Token);
            }
        }
    }
}
