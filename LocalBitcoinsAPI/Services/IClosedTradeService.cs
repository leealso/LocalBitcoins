using LocalBitcoinsAPI.Models;

namespace LocalBitcoinsAPI.Services;

public interface IClosedTradeService
{
    Task<ClosedTrade> AddAsync(ClosedTrade closedTrade, CancellationToken cancellationToken = default);

    Task<IList<ClosedTrade>> AddAsync(IList<ClosedTrade> closedTrades, CancellationToken cancellationToken = default);
    
    IQueryable<ClosedTrade> GetClosedTrades();

    int GetLatestContactId();
}
