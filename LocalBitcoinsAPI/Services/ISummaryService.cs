using LocalBitcoinsAPI.Models;

namespace LocalBitcoinsAPI.Services;

public interface ISummaryService
{
    Summary GetDaySummary(DateTime startDate);
}
