namespace coinism.Core.Models
{
    /// <summary>
    /// 전략 실행 결과에 따른 매매 액션
    /// </summary>
    public enum TradeType
    {
        None,
        Buy,
        Sell
    }

    public class TradeAction
    {
        public TradeType Type { get; set; } = TradeType.None;
        public decimal Price { get; set; }
        public decimal Volume { get; set; }
        public string Reason { get; set; } // 예: "SMA Golden Cross 발생"
    }
}
