using System;

namespace coinism.Core.Models
{
    /// <summary>
    /// 캔들(시세) 데이터 모델
    /// </summary>
    public class Candle
    {
        public DateTime Timestamp { get; set; }
        public decimal Open { get; set; }
        public decimal High { get; set; }
        public decimal Low { get; set; }
        public decimal Close { get; set; }
        public decimal Volume { get; set; }
        public string Market { get; set; }
        public string Interval { get; set; } // 예: "15m", "1h", "day"
    }
}
