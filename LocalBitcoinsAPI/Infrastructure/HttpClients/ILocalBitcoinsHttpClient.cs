using LocalBitcoinsAPI.Constants;
using LocalBitcoinsAPI.LocalBitcoins.Models;

namespace LocalBitcoinsAPI.Infrastructure.HttpClients;

public interface ILocalBitcoinsHttpClient
{
    Task<IList<Advertisement>> GetBuyAdsAsync(string countryCode, CancellationToken cancellationToken = default)
        => GetAdsAsync(TradeType.Buy, countryCode, cancellationToken);

    Task<IList<Advertisement>> GetSellAdsAsync(string countryCode, CancellationToken cancellationToken = default)
        => GetAdsAsync(TradeType.Sell, countryCode, cancellationToken);

    Task<IList<Advertisement>> GetAdsAsync(string tradeType, string countryCode, CancellationToken cancellationToken = default);
}
