using System;
using System.Text.Json.Serialization;

namespace coinism.Upbit.Models.Transfer
{
    public class UpbitDeposit
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }  // "deposit"

        [JsonPropertyName("uuid")]
        public string Uuid { get; set; }

        [JsonPropertyName("currency")]
        public string Currency { get; set; }

        [JsonPropertyName("txid")]
        public string TxId { get; set; }

        [JsonPropertyName("state")]
        public string State { get; set; } // "processing", "done"

        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("done_at")]
        public DateTime? DoneAt { get; set; }

        [JsonPropertyName("amount")]
        public string Amount { get; set; }
    }
}
