using TradesWorker.Constants;
using TradesWorker.Models;

namespace TradesWorker.HttpClients;

public interface ILocalBitcoinsHttpClient
{
    Task<IList<LocalBitcoinsTrade>> GetTradesAsync(int maxTransactionId = 0, string currencyCode = Default.CurrencyCode, CancellationToken cancellationToken = default);

    Task<IList<LocalBitcoinsTrade>> GetTradesAsync(int maxTransactionId = 0, CancellationToken cancellationToken = default) =>
        GetTradesAsync(maxTransactionId, Default.CurrencyCode, cancellationToken);

    Task<IList<LocalBitcoinsTrade>> GetTradesAsync(CancellationToken cancellationToken = default) =>
        GetTradesAsync(default, Default.CurrencyCode, cancellationToken);
}
