using LocalBitcoins.Functions.Constants;
using LocalBitcoins.Functions.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using System.Threading;

namespace LocalBitcoins.Functions.Infrastructure.HttpClients;

public interface IBccrHttpClient
{
    Task<IList<BccrIndicator>> GetExchangeRateAsync(DateTime startDate, DateTime endDate, int indicatorCode = Default.IndicatorCode, CancellationToken cancellationToken = default);

    Task<IList<BccrIndicator>> GetExchangeRateAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default)
        => GetExchangeRateAsync(startDate, endDate, Default.IndicatorCode, cancellationToken);

    Task<IList<BccrIndicator>> GetExchangeRateAsync(DateTime date, CancellationToken cancellationToken = default)
        => GetExchangeRateAsync(date, date, Default.IndicatorCode, cancellationToken);

    Task<IList<BccrIndicator>> GetExchangeRateAsync(DateTime date, int indicatorCode = Default.IndicatorCode, CancellationToken cancellationToken = default)
        => GetExchangeRateAsync(date, date, indicatorCode, cancellationToken);
}
