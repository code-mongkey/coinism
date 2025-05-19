using System;
using System.Collections.Generic;
using System.Linq;
using coinism.Core.Base;
using coinism.Core.Models;

namespace coinism.Strategies
{
    /// <summary>
    /// 볼린저 밴드 상단/하단 돌파 기반 매매 전략
    /// </summary>
    public class BollingerBandStrategy : StrategyBase<Candle, TradeAction, StrategyResult>
    {
        private readonly Queue<decimal> _prices = new();
        private int _period = 20;
        private decimal _stdDevMultiplier = 2;

        protected override StrategyResult CreateDefaultResult()
        {
            return new StrategyResult();
        }

        protected override TradeAction ExecuteCore(Candle input)
        {
            _prices.Enqueue(input.Close);
            if (_prices.Count > _period)
                _prices.Dequeue();

            if (_prices.Count < _period)
                return new TradeAction { Type = TradeType.None };

            var mean = _prices.Average();
            var stddev = (decimal)Math.Sqrt(_prices.Select(p => Math.Pow((double)(p - mean), 2)).Average());
            var upper = mean + _stdDevMultiplier * stddev;
            var lower = mean - _stdDevMultiplier * stddev;

            var action = new TradeAction { Type = TradeType.None };

            if (input.Close > upper)
            {
                action.Type = TradeType.Sell;
                action.Reason = "볼린저밴드 상단 돌파";
            }
            else if (input.Close < lower)
            {
                action.Type = TradeType.Buy;
                action.Reason = "볼린저밴드 하단 돌파";
            }

            if (action.Type != TradeType.None)
            {
                Result.TotalTrades++;
                if (action.Type == TradeType.Buy) Result.Wins++;
                else Result.Losses++;
            }

            action.Price = input.Close;
            action.Volume = 0;
            return action;
        }
    }
}
