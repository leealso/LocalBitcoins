using System;
using System.Threading;
using System.Threading.Tasks;

namespace LocalBitcoins.Functions.Services;

public interface IExchangeRateService
{
    Task UpdateExchangeRateAsync(CancellationToken cancellationToken = default);

    Task UpdateExchangeRatesAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);
}
