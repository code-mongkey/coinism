using coinism.Core.Base;
using coinism.Core.Models;

namespace coinism.Strategies
{
    /// <summary>
    /// SMA + 볼린저밴드 전략: 두 전략이 동시에 같은 방향일 때 매수/매도 수행
    /// </summary>
    public class SmaBollingerCombo : StrategyBase<Candle, TradeAction, StrategyResult>
    {
        private readonly SmaCrossStrategy _sma = new();
        private readonly BollingerBandStrategy _bb = new();

        protected override StrategyResult CreateDefaultResult()
        {
            return new StrategyResult();
        }

        public override void Init(Settings settings)
        {
            base.Init(settings);
            _sma.Init(settings);
            _bb.Init(settings);
        }

        protected override TradeAction ExecuteCore(Candle input)
        {
            var smaAction = _sma.Execute(input);
            var bbAction = _bb.Execute(input);

            TradeAction finalAction = new TradeAction { Type = TradeType.None };

            if (smaAction.Type == TradeType.Buy && bbAction.Type == TradeType.Buy)
            {
                finalAction.Type = TradeType.Buy;
                finalAction.Reason = "SMA & 볼린저 → 매수 동시 발생";
            }
            else if (smaAction.Type == TradeType.Sell && bbAction.Type == TradeType.Sell)
            {
                finalAction.Type = TradeType.Sell;
                finalAction.Reason = "SMA & 볼린저 → 매도 동시 발생";
            }

            if (finalAction.Type != TradeType.None)
            {
                Result.TotalTrades++;
                if (finalAction.Type == TradeType.Buy) Result.Wins++;
                else Result.Losses++;
            }

            finalAction.Price = input.Close;
            finalAction.Volume = 0;
            return finalAction;
        }
    }
}
