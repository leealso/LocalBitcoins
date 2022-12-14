using LocalBitcoinsAPI.Extensions;
using LocalBitcoinsAPI.Infrastructure.Data;
using LocalBitcoinsAPI.Models;
using LocalBitcoinsAPI.Utilities;
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
        var currentClosedTrade = await _dbContext.ClosedTrades
            .SingleOrDefaultAsync(x => x.ContactId == closedTrade.ContactId, cancellationToken);
        if (currentClosedTrade == null)
            await _dbContext.AddAsync(closedTrade, cancellationToken);
        
        var trade = await _dbContext.Trades.SingleOrDefaultAsync(x => 
            x.Date == closedTrade.ClosedAt
            && x.AmountBtc == closedTrade.AmountBtc
            && x.CurrencyCode == closedTrade.CurrencyCode
            && x.ContactId == null,
            cancellationToken
        );
        if (trade != null)
        {
            trade.ContactId = closedTrade.ContactId;
            _dbContext.Update(trade);
        }

        if (currentClosedTrade == null || trade != null)   
            await _dbContext.SaveChangesAsync(cancellationToken);
            
        return closedTrade;
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

    public async Task<ClosedTrade> GetAsync(int contactId, CancellationToken cancellationToken = default)
    {
        var closedTrade = await _dbContext.ClosedTrades.SingleOrDefaultAsync(x => x.ContactId == contactId, cancellationToken);
        if (closedTrade == null)
            throw QueryExceptionUtility.NotFoundException($"Closed trade {contactId} could not be found");
        return closedTrade;
    }

    public async Task<IList<ClosedTrade>> GetAsync(IReadOnlyList<int> contactIds, CancellationToken cancellationToken = default)
    {
        return await _dbContext.ClosedTrades.Where(x => contactIds.Contains(x.ContactId)).ToListAsync(cancellationToken);
    }

    public IQueryable<ClosedTrade> GetClosedTrades()
    {
        return _dbContext.ClosedTrades.AsQueryable();
    }

    public int GetLatestContactId()
    {
        return _dbContext.ClosedTrades.Max(x => x.ContactId);
    }

    public ValueTask DisposeAsync()
    {
        return _dbContext.DisposeAsync();
    }
}
