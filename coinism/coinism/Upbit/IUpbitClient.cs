using System.Collections.Generic;
using System.Threading.Tasks;
using coinism.Upbit.Models.Market;
using coinism.Upbit.Models.Trade;
using coinism.Upbit.Models.Account;
using coinism.Upbit.Models.Transfer;

namespace coinism.Upbit
{
    public interface IUpbitClient
    {
        // 📈 마켓/시세 정보
        Task<List<UpbitMarketInfo>> GetMarketsAsync();
        Task<List<UpbitCandle>> GetCandlesAsync(string market, string interval, int count = 200);
        Task<List<UpbitTicker>> GetTickersAsync(string[] markets);
        Task<List<UpbitOrderbook>> GetOrderbookAsync(string[] markets);

        // 💰 주문/체결
        Task<UpbitOrderResponse> BuyMarketOrderAsync(string market, decimal krwAmount);
        Task<UpbitOrderResponse> SellMarketOrderAsync(string market, decimal volume);
        Task<UpbitOrderDetail> GetOrderAsync(string uuid);
        Task<bool> CancelOrderAsync(string uuid);

        // 🧾 계좌/잔고
        Task<List<UpbitBalance>> GetBalancesAsync();
        Task<UpbitAccount> GetAccountAsync();
        Task<UpbitWalletState> GetWalletStatusAsync();

        // 💸 입출금
        Task<UpbitCryptoAddress> GetDepositAddressAsync(string currency);
        Task<UpbitWithdraw> RequestWithdrawAsync(string currency, decimal amount, string address);
        Task<UpbitDeposit> GetDepositStatusAsync(string txid);
    }
}
