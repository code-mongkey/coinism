using System.Text.Json.Serialization;

namespace coinism.Upbit.Models.Market
{
    public class UpbitOrderbookUnit
    {
        [JsonPropertyName("ask_price")]
        public decimal AskPrice { get; set; }

        [JsonPropertyName("bid_price")]
        public decimal BidPrice { get; set; }

        [JsonPropertyName("ask_size")]
        public decimal AskSize { get; set; }

        [JsonPropertyName("bid_size")]
        public decimal BidSize { get; set; }
    }
}
