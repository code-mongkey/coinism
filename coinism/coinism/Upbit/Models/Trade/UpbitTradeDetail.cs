using System;
using System.Text.Json.Serialization;

namespace coinism.Upbit.Models.Trade
{
    public class UpbitTradeDetail
    {
        [JsonPropertyName("market")]
        public string Market { get; set; }

        [JsonPropertyName("uuid")]
        public string Uuid { get; set; }

        [JsonPropertyName("price")]
        public string Price { get; set; }

        [JsonPropertyName("volume")]
        public string Volume { get; set; }

        [JsonPropertyName("funds")]
        public string Funds { get; set; }

        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("side")]
        public string Side { get; set; }
    }
}
