namespace coinism.Core.Models
{
    /// <summary>
    /// 설정 정보 (settings.json에서 로드됨)
    /// </summary>
    public class Settings
    {
        public string ApiKey { get; set; }
        public string ApiSecret { get; set; }
        public string SlackUrl { get; set; }
        public decimal PercentToUse { get; set; }
        public string RunMode { get; set; } // "Live" or "Backtest"
        public string[] StrategyPlugins { get; set; }
        public int IntervalSeconds { get; set; } = 60; // 기본 주기
    }
}
