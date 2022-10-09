using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LocalBitcoins.Functions.Constants;
using LocalBitcoins.Functions.Models;

namespace LocalBitcoins.Functions.Infrastructure.HttpClients;

public interface ILocalBitcoinsHttpClient
{
    Task<IList<LocalBitcoinsTrade>> GetTradesAsync(int maxTransactionId = 0, string currencyCode = CurrencyCode.CRC, CancellationToken cancellationToken = default);

    Task<IList<LocalBitcoinsTrade>> GetTradesAsync(int maxTransactionId = 0, CancellationToken cancellationToken = default) =>
        GetTradesAsync(maxTransactionId, CurrencyCode.CRC, cancellationToken);

    Task<IList<LocalBitcoinsTrade>> GetTradesAsync(CancellationToken cancellationToken = default) =>
        GetTradesAsync(default, CurrencyCode.CRC, cancellationToken);

    Task<IList<LocalBitcoinsContactData>> GetReleasedTradesAsync(DateTime? startDate = null, CancellationToken cancellationToken = default);

    Task<IList<LocalBitcoinsContactData>> GetReleasedTradesAsync(CancellationToken cancellationToken = default)
        => GetReleasedTradesAsync(default, cancellationToken);
}
