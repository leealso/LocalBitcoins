using LocalBitcoinsAPI.Extensions;
using LocalBitcoinsAPI.Infrastructure.Data;
using LocalBitcoinsAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LocalBitcoinsAPI.Services;

public class ClosedTradeService : IClosedTradeService, IAsyncDisposable
{
    private readonly LocalBitcoinsDbContext _dbContext;

    public ClosedTradeService(IDbContextFactory<LocalBitcoinsDbContext> dbContextFactory)
    {
        _dbContext = dbContextFactory.CreateDbContext();
    }

    public async Task<ClosedTrade> AddAsync(ClosedTrade closedTrade, CancellationToken cancellationToken = default)
    {
        var result = await _dbContext.AddAsync(closedTrade, cancellationToken);
        
        var trade = await _dbContext.Trades.SingleOrDefaultAsync(x => 
            x.Date == closedTrade.ClosedAt
            && x.AmountBtc == closedTrade.AmountBtc
            && x.AmountFiat == closedTrade.AmountFiat
            && x.CurrencyCode == closedTrade.CurrencyCode,
            cancellationToken
        );
        if (trade != null)
            trade.ContactId = closedTrade.ContactId;
            
        await _dbContext.SaveChangesAsync(cancellationToken);
        return result.Entity;
    }

    public async Task<IList<ClosedTrade>> AddAsync(IList<ClosedTrade> closedTrades, CancellationToken cancellationToken = default)
    {
        var addedClosedTrades = new List<ClosedTrade>();
        
        await foreach (var closedTrade in closedTrades.ToAsyncEnumerable())
        {
            var addedClosedTrade = await AddAsync(closedTrade, cancellationToken);
            addedClosedTrades.Add(addedClosedTrade);
        }

        return addedClosedTrades;
    }

    public IQueryable<ClosedTrade> GetClosedTrades()
    {
        return _dbContext.ClosedTrades.AsQueryable();
    }

    public IList<int> GetMissingContactIds(DateTime closedAt, IList<int> contactIds)
    {
        var existingContactIds = _dbContext.ClosedTrades.Where(x => x.ClosedAt >= closedAt)
            .Select(x => x.ContactId);
        return contactIds.Except(existingContactIds).ToList();
    }

    public ValueTask DisposeAsync()
    {
        return _dbContext.DisposeAsync();
    }
}
