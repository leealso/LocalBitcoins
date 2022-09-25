using LocalBitcoinsAPI.Infrastructure.Data;
using LocalBitcoinsAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LocalBitcoinsAPI.Services;

public class DailySummaryService : IDailySummaryService, IAsyncDisposable
{
    private const int timeZone = -6;
    private readonly LocalBitcoinsDbContext _dbContext;

    public DailySummaryService(IDbContextFactory<LocalBitcoinsDbContext> dbContextFactory)
    {
        _dbContext = dbContextFactory.CreateDbContext();
    }

    public DailySummary GetDailySummary(DateTime date)
    {
        var endDate = date.AddDays(1);
        var trades = _dbContext.Trades.Where(x => x.Date >= date && x.Date < endDate);
        var yesterdayStartDate = date.AddDays(-1);
        var yesterdayTrades = _dbContext.Trades.Where(x => x.Date >= yesterdayStartDate && x.Date < date);
        return new DailySummary (date, trades, yesterdayTrades);
    }

    public ValueTask DisposeAsync()
    {
        return _dbContext.DisposeAsync();
    }
}
