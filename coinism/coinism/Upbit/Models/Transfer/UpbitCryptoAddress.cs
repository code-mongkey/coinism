using System.Text.Json.Serialization;

namespace coinism.Upbit.Models.Transfer
{
    public class UpbitCryptoAddress
    {
        [JsonPropertyName("currency")]
        public string Currency { get; set; }

        [JsonPropertyName("deposit_address")]
        public string DepositAddress { get; set; }

        [JsonPropertyName("secondary_address")]
        public string SecondaryAddress { get; set; }

        [JsonPropertyName("destination_tag")]
        public string DestinationTag { get; set; } // 일부 코인 전용 (e.g., XRP)
    }
}
