using System;
using System.Threading;
using System.Threading.Tasks;

namespace LocalBitcoins.Functions.Services;

public interface IClosedTradeService
{
    Task UpdateClosedTradesAsync(CancellationToken cancellationToken = default);

    Task UpdateClosedTradesAsync(DateTime startDate, CancellationToken cancellationToken = default);
}
