using System.Text.Json.Serialization;

namespace coinism.Upbit.Models.Account
{
    public class UpbitWalletState
    {
        [JsonPropertyName("currency")]
        public string Currency { get; set; }

        [JsonPropertyName("wallet_state")]
        public string WalletState { get; set; } // "working", "withdraw_only", "deposit_only", "paused"

        [JsonPropertyName("block_state")]
        public string BlockState { get; set; } // "normal", "delay", "suspend"

        [JsonPropertyName("reason")]
        public string Reason { get; set; }
    }
}
