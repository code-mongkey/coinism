using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace coinism.Upbit.Models.Account
{
    public class UpbitAccount
    {
        [JsonPropertyName("balances")]
        public List<UpbitBalance> Balances { get; set; }

        [JsonPropertyName("currency")]
        public string Currency { get; set; }

        [JsonPropertyName("account_id")]
        public string AccountId { get; set; }

        [JsonPropertyName("uuid")]
        public string Uuid { get; set; }
    }
}
