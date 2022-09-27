using LocalBitcoinsAPI.Models;
using LocalBitcoinsAPI.Services;

namespace LocalBitcoinsAPI.GraphQL;

public class LocalBitcoinsQuery
{
    [UseOffsetPaging(MaxPageSize = 100, IncludeTotalCount = true)]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Trade> GetTrades([Service] ITradeService queryService)
    {
        return queryService.GetTrades();
    }

    [UseOffsetPaging(MaxPageSize = 100, IncludeTotalCount = true)]
    [UseFiltering]
    [UseSorting]
    public IQueryable<ClosedTrade> GetClosedTrades([Service] IClosedTradeService queryService)
    {
        return queryService.GetClosedTrades();
    }

    public DailySummary GetDailySummary(DateTime date, [Service] IDailySummaryService queryService)
    {
        return queryService.GetDailySummary(date);
    }

    [UseOffsetPaging(MaxPageSize = 100, IncludeTotalCount = true)]
    [UseFiltering]
    [UseSorting]
    public async Task<IQueryable<Advertisement>> GetBuyAdvertisementsAsync(string countryCode, [Service] IAdvertisementService queryService, CancellationToken cancellationToken = default)
    {
        return await queryService.GetBuyAdvertisementsAsync(countryCode, cancellationToken);
    }

    [UseOffsetPaging(MaxPageSize = 100, IncludeTotalCount = true)]
    [UseFiltering]
    [UseSorting]
    public async Task<IQueryable<Advertisement>> GetSellAdvertisementsAsync(string countryCode, [Service] IAdvertisementService queryService, CancellationToken cancellationToken = default)
    {
        return await queryService.GetSellAdvertisementsAsync(countryCode, cancellationToken);
    }

    public async Task<Quote> GetQuotesAsync([Service] IQuoteService queryService, CancellationToken cancellationToken = default)
    {
        return await queryService.GetQuoteAsync(default, cancellationToken);
    }
}