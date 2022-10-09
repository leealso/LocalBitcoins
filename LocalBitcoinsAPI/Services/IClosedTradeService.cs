using LocalBitcoinsAPI.Models;

namespace LocalBitcoinsAPI.Services;

public interface IClosedTradeService
{
    Task<ClosedTrade> AddAsync(ClosedTrade closedTrade, CancellationToken cancellationToken = default);

    Task<IList<ClosedTrade>> AddAsync(IList<ClosedTrade> closedTrades, CancellationToken cancellationToken = default);
    
    Task<ClosedTrade> GetAsync(int contactId, CancellationToken cancellationToken = default);

    Task<IList<ClosedTrade>> GetAsync(IReadOnlyList<int> contactIds, CancellationToken cancellationToken = default);

    Task<IList<ClosedTrade>> GetAsync(IReadOnlyList<int> contactIds)
        => GetAsync(contactIds);
    
    IQueryable<ClosedTrade> GetClosedTrades();

    int GetLatestContactId();
}
