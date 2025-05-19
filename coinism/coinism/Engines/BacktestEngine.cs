using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using coinism.Core.Interfaces;
using coinism.Core.Models;

namespace coinism.Engines
{
    /// <summary>
    /// 백테스트용 엔진
    /// </summary>
    public class BacktestEngine : IEngine
    {
        private readonly IEnumerable<Candle> _historicalData;
        private readonly IStrategy<Candle, TradeAction, StrategyResult> _strategy;

        public BacktestEngine(
            IEnumerable<Candle> historicalData,
            IStrategy<Candle, TradeAction, StrategyResult> strategy)
        {
            _historicalData = historicalData;
            _strategy = strategy;
        }

        public async Task RunAsync(CancellationToken token)
        {
            _strategy.Init(new Settings());

            foreach (var candle in _historicalData)
            {
                if (token.IsCancellationRequested)
                    break;

                if (_strategy.ShouldExecute(candle))
                {
                    TradeAction action = _strategy.Execute(candle);
                    Console.WriteLine($"[Backtest] {action.Type} @ {action.Price} ({action.Reason})");
                }

                await Task.Delay(1, token); // 최소 딜레이
            }

            var result = _strategy.GetResult();
            Console.WriteLine($"[Backtest Result] {result}");
        }
    }
}
