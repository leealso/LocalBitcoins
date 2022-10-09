using LocalBitcoinsAPI.Models;

namespace LocalBitcoinsAPI.Services;

public interface ITradeService
{
    Task<Trade> AddAsync(Trade trade, CancellationToken cancellationToken = default);

    Task<IList<Trade>> AddAsync(IList<Trade> trades, CancellationToken cancellationToken = default);

    int GetLatestTransactionId();

    IQueryable<Trade> GetTrades();
}
