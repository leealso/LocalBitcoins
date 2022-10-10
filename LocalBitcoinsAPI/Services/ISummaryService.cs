using LocalBitcoinsAPI.Models;

namespace LocalBitcoinsAPI.Services;

public interface ISummaryService
{
    Summary GetSummary(DateTime startDate, DateTime endDate);

    Summary GetSummary(DateTime date)
        => GetSummary(date, date.AddDays(1));
}
