using LocalBitcoins.Functions.Constants;
using LocalBitcoins.Functions.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using System.Threading;

namespace LocalBitcoins.Functions.Infrastructure.HttpClients;

public interface IBccrHttpClient
{
    Task<IList<Indicator>> GetExchangeRateAsync(DateTime startDate, DateTime endDate, int indicatorCode = Default.IndicatorCode, CancellationToken cancellationToken = default);

    Task<IList<Indicator>> GetExchangeRateAsync(DateTime date, int indicatorCode = Default.IndicatorCode, CancellationToken cancellationToken = default)
        => GetExchangeRateAsync(date, date, indicatorCode, cancellationToken);
}
