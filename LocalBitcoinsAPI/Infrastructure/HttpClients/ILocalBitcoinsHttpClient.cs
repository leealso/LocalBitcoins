using LocalBitcoinsAPI.Constants;
using LocalBitcoinsAPI.Models.LocalBitcoins;

namespace LocalBitcoinsAPI.Infrastructure.HttpClients;

public interface ILocalBitcoinsHttpClient
{
    Task<IEnumerable<Advertisement>> GetBuyAdsAsync(string countryCode, CancellationToken cancellationToken = default)
        => GetAdsAsync(TradeType.Buy, countryCode, cancellationToken);

    Task<IEnumerable<Advertisement>> GetSellAdsAsync(string countryCode, CancellationToken cancellationToken = default)
        => GetAdsAsync(TradeType.Sell, countryCode, cancellationToken);

    Task<IEnumerable<Advertisement>> GetAdsAsync(string tradeType, string countryCode, CancellationToken cancellationToken = default);
}
