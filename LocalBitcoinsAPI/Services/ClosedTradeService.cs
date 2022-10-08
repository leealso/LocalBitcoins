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

    public IQueryable<ClosedTrade> GetClosedTrades()
    {
        return _dbContext.ClosedTrades.AsQueryable();
    }

    public ValueTask DisposeAsync()
    {
        return _dbContext.DisposeAsync();
    }
}
