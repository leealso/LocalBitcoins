using LocalBitcoinsAPI.Infrastructure.Data;
using LocalBitcoinsAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LocalBitcoinsAPI.Services;

public class SummaryService : ISummaryService, IAsyncDisposable
{
    private readonly LocalBitcoinsDbContext _dbContext;

    public SummaryService(IDbContextFactory<LocalBitcoinsDbContext> dbContextFactory)
    {
        _dbContext = dbContextFactory.CreateDbContext();
    }

    public Summary GetDaySummary(DateTime startDate)
    {
        var endDate = startDate.AddDays(1);
        var trades = _dbContext.Trades.Where(x => x.Date >= startDate && x.Date < endDate);
        return new Summary(startDate, endDate, trades);
    }

    public ValueTask DisposeAsync()
    {
        return _dbContext.DisposeAsync();
    }
}
