using LocalBitcoinsAPI.Infrastructure.Data;
using LocalBitcoinsAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LocalBitcoinsAPI.Services;

public class DailySummaryService : IDailySummaryService, IAsyncDisposable
{
    private readonly LocalBitcoinsDbContext _dbContext;

    public DailySummaryService(IDbContextFactory<LocalBitcoinsDbContext> dbContextFactory)
    {
        _dbContext = dbContextFactory.CreateDbContext();
    }

    public DailySummary GetDailySummary(DateTime date)
    {
        var dateOnly = DateOnly.FromDateTime(date);
        var trades = _dbContext.Trades.Where(x => DateOnly.FromDateTime(x.Date) == dateOnly);
        return new DailySummary 
        {
            Date = date.Date,
            TransactionCount = trades.Count(),
            BtcVolume = trades.Sum(x => x.AmountBtc),
            FiatVolume = trades.Sum(x => x.AmountFiat)
        };
    }

    public ValueTask DisposeAsync()
    {
        return _dbContext.DisposeAsync();
    }
}
