using System.Collections.Generic;
using System.Linq;
using coinism.Core.Base;
using coinism.Core.Models;

namespace coinism.Strategies
{
    /// <summary>
    /// 단기/장기 이동평균선 교차 기반 전략 (SMA 골든/데드크로스)
    /// </summary>
    public class SmaCrossStrategy : StrategyBase<Candle, TradeAction, StrategyResult>
    {
        private readonly Queue<decimal> _shortPrices = new();
        private readonly Queue<decimal> _longPrices = new();

        private int _shortPeriod = 5;
        private int _longPeriod = 15;

        private decimal? _prevShortSma = null;
        private decimal? _prevLongSma = null;

        protected override StrategyResult CreateDefaultResult()
        {
            return new StrategyResult();
        }

        protected override TradeAction ExecuteCore(Candle input)
        {
            _shortPrices.Enqueue(input.Close);
            _longPrices.Enqueue(input.Close);

            if (_shortPrices.Count > _shortPeriod)
                _shortPrices.Dequeue();
            if (_longPrices.Count > _longPeriod)
                _longPrices.Dequeue();

            if (_shortPrices.Count < _shortPeriod || _longPrices.Count < _longPeriod)
                return new TradeAction { Type = TradeType.None };

            decimal shortSma = _shortPrices.Average();
            decimal longSma = _longPrices.Average();

            TradeAction action = new TradeAction { Type = TradeType.None };

            if (_prevShortSma.HasValue && _prevLongSma.HasValue)
            {
                bool prevGolden = _prevShortSma <= _prevLongSma;
                bool currGolden = shortSma > longSma;

                if (prevGolden && currGolden)
                {
                    action.Type = TradeType.Buy;
                    action.Reason = "SMA 골든크로스 발생";
                }

                bool prevDead = _prevShortSma >= _prevLongSma;
                bool currDead = shortSma < longSma;

                if (prevDead && currDead)
                {
                    action.Type = TradeType.Sell;
                    action.Reason = "SMA 데드크로스 발생";
                }
            }

            _prevShortSma = shortSma;
            _prevLongSma = longSma;

            if (action.Type != TradeType.None)
            {
                Result.TotalTrades++;
                if (action.Type == TradeType.Buy)
                    Result.Wins++;
                else
                    Result.Losses++;
            }

            action.Price = input.Close;
            action.Volume = 0; // 실제 매매 시점에서 설정
            return action;
        }
    }
}
