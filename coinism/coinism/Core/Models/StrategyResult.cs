namespace coinism.Core.Models
{
    /// <summary>
    /// 전략의 누적 결과 또는 리포트용 데이터
    /// </summary>
    public class StrategyResult
    {
        public int TotalTrades { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public decimal TotalProfit { get; set; }

        public override string ToString()
        {
            return $"총 트레이드: {TotalTrades}, 승: {Wins}, 패: {Losses}, 수익: {TotalProfit:N2}";
        }
    }
}
