using System;
using System.Threading;
using System.Threading.Tasks;
using coinism.Core.Interfaces;
using coinism.Core.Models;

namespace coinism.Engines
{
    /// <summary>
    /// 실시간 자동매매 엔진
    /// </summary>
    public class LiveTradingEngine : IEngine
    {
        private readonly IStrategy<Candle, TradeAction, StrategyResult> _strategy;
        private readonly Func<Task<Candle>> _getLatestCandleAsync;
        private readonly Func<TradeAction, Task> _executeTradeAsync;
        private readonly int _intervalSeconds;

        public LiveTradingEngine(
            IStrategy<Candle, TradeAction, StrategyResult> strategy,
            Func<Task<Candle>> getLatestCandleAsync,
            Func<TradeAction, Task> executeTradeAsync,
            int intervalSeconds = 60)
        {
            _strategy = strategy;
            _getLatestCandleAsync = getLatestCandleAsync;
            _executeTradeAsync = executeTradeAsync;
            _intervalSeconds = intervalSeconds;
        }

        public async Task RunAsync(CancellationToken token)
        {
            _strategy.Init(new Settings());

            while (!token.IsCancellationRequested)
            {
                try
                {
                    var candle = await _getLatestCandleAsync();

                    if (_strategy.ShouldExecute(candle))
                    {
                        var action = _strategy.Execute(candle);
                        Console.WriteLine($"[LIVE] {action.Type} @ {action.Price} ({action.Reason})");

                        if (action.Type != TradeType.None)
                            await _executeTradeAsync(action);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[ERROR] {ex.Message}");
                }

                await Task.Delay(TimeSpan.FromSeconds(_intervalSeconds), token);
            }
        }
    }
}
