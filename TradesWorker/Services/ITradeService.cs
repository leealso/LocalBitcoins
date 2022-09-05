using TradesWorker.Models;

namespace TradesWorker.Services;

public interface ITradeService
{
    Task<IList<Trade>> UpdateTradesAsync(CancellationToken cancellationToken = default);
}
