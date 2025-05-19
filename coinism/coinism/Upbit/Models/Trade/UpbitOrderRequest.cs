using System.Text.Json.Serialization;

namespace coinism.Upbit.Models.Trade
{
    public class UpbitOrderRequest
    {
        [JsonPropertyName("market")]
        public string Market { get; set; }

        [JsonPropertyName("side")]
        public string Side { get; set; } // "bid" or "ask"

        [JsonPropertyName("volume")]
        public string Volume { get; set; } // 지정가: 필수, 시장가 매수: null

        [JsonPropertyName("price")]
        public string Price { get; set; } // 시장가 매수: KRW, 지정가: 단가

        [JsonPropertyName("ord_type")]
        public string OrderType { get; set; } // "limit", "price", "market"
    }
}
