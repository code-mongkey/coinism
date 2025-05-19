using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using coinism.Upbit.Models.Market;
using coinism.Upbit.Models.Trade;
using coinism.Upbit.Models.Account;
using coinism.Upbit.Models.Transfer;

namespace coinism.Upbit
{
    public class UpbitClient : IUpbitClient
    {
        private readonly HttpClient _http;
        private readonly string _accessKey;
        private readonly string _secretKey;

        public UpbitClient(string accessKey, string secretKey)
        {
            _accessKey = accessKey;
            _secretKey = secretKey;
            _http = new HttpClient { BaseAddress = new Uri("https://api.upbit.com") };
        }

        private async Task<T> GetAsync<T>(string endpoint)
        {
            var response = await _http.GetAsync(endpoint);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(json);
        }

        private async Task<T> AuthPostAsync<T>(string endpoint, Dictionary<string, string> parameters = null)
        {
            var token = JwtTokenGenerator.Generate(_accessKey, _secretKey, parameters);

            HttpContent content = null;
            if (parameters != null && parameters.Count > 0)
            {
                content = new FormUrlEncodedContent(parameters);
            }

            var request = new HttpRequestMessage(HttpMethod.Post, endpoint)
            {
                Content = content // null일 수도 있음
            };
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _http.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(json);
        }


        private async Task<T> AuthGetAsync<T>(string endpoint, Dictionary<string, string> parameters)
        {
            var query = string.Join("&", parameters.Select(kv => $"{kv.Key}={kv.Value}"));
            var token = JwtTokenGenerator.Generate(_accessKey, _secretKey, parameters);
            var request = new HttpRequestMessage(HttpMethod.Get, endpoint + "?" + query);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _http.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(json);
        }

        public Task<List<UpbitMarketInfo>> GetMarketsAsync() => GetAsync<List<UpbitMarketInfo>>("/v1/market/all");

        public Task<List<UpbitCandle>> GetCandlesAsync(string market, string interval, int count = 200) =>
            GetAsync<List<UpbitCandle>>($"/v1/candles/{interval}?market={market}&count={count}");

        public Task<List<UpbitTicker>> GetTickersAsync(string[] markets)
        {
            var joined = string.Join(",", markets);
            return GetAsync<List<UpbitTicker>>($"/v1/ticker?markets={joined}");
        }

        public Task<List<UpbitOrderbook>> GetOrderbookAsync(string[] markets)
        {
            var joined = string.Join(",", markets);
            return GetAsync<List<UpbitOrderbook>>($"/v1/orderbook?markets={joined}");
        }

        public Task<UpbitOrderResponse> BuyMarketOrderAsync(string market, decimal krwAmount)
        {
            var parameters = new Dictionary<string, string>
            {
                { "market", market },
                { "side", "bid" },
                { "price", krwAmount.ToString() },
                { "ord_type", "price" }
            };
            return AuthPostAsync<UpbitOrderResponse>("/v1/orders", parameters);
        }

        public Task<UpbitOrderResponse> SellMarketOrderAsync(string market, decimal volume)
        {
            var parameters = new Dictionary<string, string>
            {
                { "market", market },
                { "side", "ask" },
                { "volume", volume.ToString() },
                { "ord_type", "market" }
            };
            return AuthPostAsync<UpbitOrderResponse>("/v1/orders", parameters);
        }

        public Task<UpbitOrderDetail> GetOrderAsync(string uuid)
        {
            var parameters = new Dictionary<string, string>
            {
                { "uuid", uuid }
            };
            return AuthGetAsync<UpbitOrderDetail>("/v1/order", parameters);
        }

        public async Task<bool> CancelOrderAsync(string uuid)
        {
            var parameters = new Dictionary<string, string>
            {
                { "uuid", uuid }
            };
            var result = await AuthPostAsync<UpbitOrderResponse>("/v1/order", parameters);
            return result.State == "cancel";
        }

        public Task<List<UpbitBalance>> GetBalancesAsync() =>
            AuthGetAsync<List<UpbitBalance>>("/v1/accounts", new());

        public Task<UpbitWalletState> GetWalletStatusAsync() =>
            AuthGetAsync<UpbitWalletState>("/v1/status/wallet", new());

        public Task<UpbitAccount> GetAccountAsync() =>
            AuthGetAsync<UpbitAccount>("/v1/account", new());

        public Task<UpbitCryptoAddress> GetDepositAddressAsync(string currency)
        {
            var parameters = new Dictionary<string, string>
            {
                { "currency", currency }
            };
            return AuthGetAsync<UpbitCryptoAddress>("/v1/deposits/coin_address", parameters);
        }

        public Task<UpbitWithdraw> RequestWithdrawAsync(string currency, decimal amount, string address)
        {
            var parameters = new Dictionary<string, string>
            {
                { "currency", currency },
                { "amount", amount.ToString() },
                { "address", address }
            };
            return AuthPostAsync<UpbitWithdraw>("/v1/withdraws/coin", parameters);
        }

        public Task<UpbitDeposit> GetDepositStatusAsync(string txid)
        {
            var parameters = new Dictionary<string, string>
            {
                { "txid", txid }
            };
            return AuthGetAsync<UpbitDeposit>("/v1/deposits", parameters);
        }
    }
}
