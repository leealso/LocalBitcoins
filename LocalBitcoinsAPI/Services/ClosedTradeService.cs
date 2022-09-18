using LocalBitcoinsAPI.Infrastructure.Data;
using LocalBitcoinsAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LocalBitcoinsAPI.Services;

public class ClosedTradeService : IClosedTradeService
{
    private readonly LocalBitcoinsDbContext _dbContext;

    public ClosedTradeService(IDbContextFactory<LocalBitcoinsDbContext> dbContextFactory)
    {
        _dbContext = dbContextFactory.CreateDbContext();
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
