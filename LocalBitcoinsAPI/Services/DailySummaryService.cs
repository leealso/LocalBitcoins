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
        var startDate = date.Date.AddHours(timeZone);
        var endDate = startDate.AddHours(24);
        var trades = _dbContext.Trades.Where(x => x.Date >= startDate && x.Date < endDate);
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
