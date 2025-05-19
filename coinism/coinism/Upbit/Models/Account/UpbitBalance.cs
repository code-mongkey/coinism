using System.Text.Json.Serialization;

namespace coinism.Upbit.Models.Account
{
    public class UpbitBalance
    {
        [JsonPropertyName("currency")]
        public string Currency { get; set; }

        [JsonPropertyName("balance")]
        public string Balance { get; set; }

        [JsonPropertyName("locked")]
        public string Locked { get; set; }

        [JsonPropertyName("avg_buy_price")]
        public string AvgBuyPrice { get; set; }

        [JsonPropertyName("avg_buy_price_modified")]
        public bool AvgBuyPriceModified { get; set; }

        [JsonPropertyName("unit_currency")]
        public string UnitCurrency { get; set; }
    }
}
