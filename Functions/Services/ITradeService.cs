using System.Threading;
using System.Threading.Tasks;

namespace LocalBitcoins.Functions.Services;

public interface ITradeService
{
    Task UpdateTradesAsync(CancellationToken cancellationToken = default);
}
