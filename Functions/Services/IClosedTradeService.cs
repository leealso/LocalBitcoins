using System.Threading;
using System.Threading.Tasks;

namespace LocalBitcoins.Functions.Services;

public interface IClosedTradeService
{
    Task UpdateClosedTradesAsync(CancellationToken cancellationToken = default);
}
