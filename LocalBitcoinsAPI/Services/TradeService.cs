using LocalBitcoinsAPI.Extensions;
using LocalBitcoinsAPI.Infrastructure.Data;
using LocalBitcoinsAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LocalBitcoinsAPI.Services;

public class TradeService : ITradeService, IAsyncDisposable
{
    private readonly LocalBitcoinsDbContext _dbContext;

    public TradeService(IDbContextFactory<LocalBitcoinsDbContext> dbContextFactory)
    {
        _dbContext = dbContextFactory.CreateDbContext();
    }

    public async Task<Trade> AddAsync(Trade trade, CancellationToken cancellationToken = default)
    {
        var currentTrade = await _dbContext.Trades
            .SingleOrDefaultAsync(x => x.TransactionId == trade.TransactionId, cancellationToken);
        if (currentTrade != null)
            return currentTrade;

        var closedTrade = await _dbContext.ClosedTrades.SingleOrDefaultAsync(x => 
            x.ClosedAt == trade.Date
            && x.AmountBtc == trade.AmountBtc
            && x.AmountFiat == trade.AmountFiat
            && x.CurrencyCode == trade.CurrencyCode,
            cancellationToken
        );
        if (closedTrade != null)
            trade.ContactId = closedTrade.ContactId;

        var result = await _dbContext.AddAsync(trade, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return result.Entity;
    }

    public async Task<IList<Trade>> AddAsync(IList<Trade> trades, CancellationToken cancellationToken = default)
    {
        var addedTrades = new List<Trade>();
        
        await foreach (var trade in trades.ToAsyncEnumerable())
        {
            var addedTrade = await AddAsync(trade, cancellationToken);
            addedTrades.Add(addedTrade);
        }

        return addedTrades;
    }

    public int GetLatestTransactionId()
    {
        return _dbContext.Trades.Max(x => x.TransactionId);
    }

    public IQueryable<Trade> GetTrades()
    {
        return _dbContext.Trades.AsQueryable();
    }

    public ValueTask DisposeAsync()
    {
        return _dbContext.DisposeAsync();
    }
}
