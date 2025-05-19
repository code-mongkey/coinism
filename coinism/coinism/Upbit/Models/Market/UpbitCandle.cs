using System;
using System.Text.Json.Serialization;

namespace coinism.Upbit.Models.Market
{
    public class UpbitCandle
    {
        [JsonPropertyName("market")]
        public string Market { get; set; }

        [JsonPropertyName("candle_date_time_kst")]
        public DateTime CandleTimeKst { get; set; }

        [JsonPropertyName("opening_price")]
        public decimal OpeningPrice { get; set; }

        [JsonPropertyName("high_price")]
        public decimal HighPrice { get; set; }

        [JsonPropertyName("low_price")]
        public decimal LowPrice { get; set; }

        [JsonPropertyName("trade_price")]
        public decimal TradePrice { get; set; }

        [JsonPropertyName("candle_acc_trade_volume")]
        public decimal Volume { get; set; }

        [JsonPropertyName("unit")]
        public int Unit { get; set; } // 분 단위 (1, 3, 5, 15, 30, 60, 240)
    }
}
