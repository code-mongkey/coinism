using System.Text.Json.Serialization;

namespace coinism.Upbit.Models.Market
{
    public class UpbitMarketInfo
    {
        [JsonPropertyName("market")]
        public string Market { get; set; }

        [JsonPropertyName("korean_name")]
        public string KoreanName { get; set; }

        [JsonPropertyName("english_name")]
        public string EnglishName { get; set; }
    }
}
