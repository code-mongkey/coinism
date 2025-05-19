using System;
using System.Text.Json.Serialization;

namespace coinism.Upbit.Models.Transfer
{
    public class UpbitWithdraw
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }  // "withdraw"

        [JsonPropertyName("uuid")]
        public string Uuid { get; set; }

        [JsonPropertyName("currency")]
        public string Currency { get; set; }

        [JsonPropertyName("txid")]
        public string TxId { get; set; }

        [JsonPropertyName("state")]
        public string State { get; set; }  // e.g., "processing", "done"

        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("done_at")]
        public DateTime? DoneAt { get; set; }

        [JsonPropertyName("amount")]
        public string Amount { get; set; }

        [JsonPropertyName("fee")]
        public string Fee { get; set; }

        [JsonPropertyName("transaction_type")]
        public string TransactionType { get; set; } // "default"
    }
}
