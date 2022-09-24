using LocalBitcoinsAPI.Models;

namespace LocalBitcoinsAPI.Services;

public interface IDailySummaryService
{
    DailySummary GetDailySummary(DateTime date);
}
