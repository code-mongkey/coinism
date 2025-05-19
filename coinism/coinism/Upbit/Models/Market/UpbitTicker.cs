using System;
using System.Text.Json.Serialization;

namespace coinism.Upbit.Models.Market
{
    public class UpbitTicker
    {
        [JsonPropertyName("market")]
        public string Market { get; set; }

        [JsonPropertyName("trade_price")]
        public decimal TradePrice { get; set; }

        [JsonPropertyName("signed_change_rate")]
        public decimal SignedChangeRate { get; set; }

        [JsonPropertyName("acc_trade_price_24h")]
        public decimal AccTradePrice24h { get; set; }

        [JsonPropertyName("acc_trade_volume_24h")]
        public decimal AccTradeVolume24h { get; set; }

        [JsonPropertyName("timestamp")]
        public long Timestamp { get; set; }

        public DateTime TimestampUtc => DateTimeOffset.FromUnixTimeMilliseconds(Timestamp).UtcDateTime;
    }
}
