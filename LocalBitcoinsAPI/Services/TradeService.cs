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

        var closedTrade = await _dbContext.ClosedTrades.SingleOrDefaultAsync(x => 
            x.ClosedAt == trade.Date
            && x.AmountBtc == trade.AmountBtc
            && x.AmountFiat == trade.AmountFiat
            && x.CurrencyCode == trade.CurrencyCode,
            cancellationToken
        );

        if (currentTrade == null)
        {
            trade.ContactId = closedTrade?.ContactId;
            await _dbContext.AddAsync(trade, cancellationToken);
        }
        else 
        {
            currentTrade.ContactId = closedTrade?.ContactId;
            _dbContext.Update(currentTrade);
        }
            
        await _dbContext.SaveChangesAsync(cancellationToken);
        return trade;
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
