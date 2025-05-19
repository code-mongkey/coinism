using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace coinism.Upbit.Models.Market
{
    public class UpbitOrderbook
    {
        [JsonPropertyName("market")]
        public string Market { get; set; }

        [JsonPropertyName("timestamp")]
        public long Timestamp { get; set; }

        [JsonPropertyName("total_ask_size")]
        public decimal TotalAskSize { get; set; }

        [JsonPropertyName("total_bid_size")]
        public decimal TotalBidSize { get; set; }

        [JsonPropertyName("orderbook_units")]
        public List<UpbitOrderbookUnit> Units { get; set; }
    }
}
