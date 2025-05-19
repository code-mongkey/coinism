using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace coinism.Notifier
{
    public static class SlackNotifier
    {
        private static readonly HttpClient _httpClient = new();

        public static async Task NotifyAsync(string webhookUrl, string message)
        {
            try
            {
                var payload = new
                {
                    text = message
                };

                var json = System.Text.Json.JsonSerializer.Serialize(payload);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync(webhookUrl, content);
                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"[SlackNotifier] 실패: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[SlackNotifier] 예외 발생: {ex.Message}");
            }
        }
    }
}
