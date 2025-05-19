using System;
using System.Text.Json.Serialization;

namespace coinism.Upbit.Models.Trade
{
    public class UpbitOrderResponse
    {
        [JsonPropertyName("uuid")]
        public string Uuid { get; set; }

        [JsonPropertyName("market")]
        public string Market { get; set; }

        [JsonPropertyName("side")]
        public string Side { get; set; }

        [JsonPropertyName("ord_type")]
        public string OrderType { get; set; }

        [JsonPropertyName("price")]
        public string Price { get; set; }

        [JsonPropertyName("state")]
        public string State { get; set; } // "wait", "done", "cancel"

        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("volume")]
        public string Volume { get; set; }

        [JsonPropertyName("remaining_volume")]
        public string RemainingVolume { get; set; }

        [JsonPropertyName("reserved_fee")]
        public string ReservedFee { get; set; }

        [JsonPropertyName("paid_fee")]
        public string PaidFee { get; set; }

        [JsonPropertyName("locked")]
        public string Locked { get; set; }

        [JsonPropertyName("executed_volume")]
        public string ExecutedVolume { get; set; }

        [JsonPropertyName("trades_count")]
        public int TradesCount { get; set; }
    }
}
