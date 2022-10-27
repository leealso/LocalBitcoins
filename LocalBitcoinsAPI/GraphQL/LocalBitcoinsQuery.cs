using LocalBitcoinsAPI.Constants;
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

    public int GetLatestTransactionId([Service] ITradeService queryService)
    {
        return queryService.GetLatestTransactionId();
    }

    [UseOffsetPaging(MaxPageSize = 100, IncludeTotalCount = true)]
    [UseFiltering]
    [UseSorting]
    public IQueryable<ClosedTrade> GetClosedTrades([Service] IClosedTradeService queryService)
    {
        return queryService.GetClosedTrades();
    }

    public int GetLatestContactId([Service] IClosedTradeService queryService)
    {
        return queryService.GetLatestContactId();
    }

    public Summary GetDaySummary(DateTime date, [Service] ISummaryService queryService)
    {
        return queryService.GetSummary(date);
    }

    [UseSorting]
    public IList<Summary> GetDailySummaries(DateTime startDate, DateTime endDate, [Service] ISummaryService queryService)
    {
        return queryService.GetDailySummaries(startDate, endDate);
    }

    public Summary GetSummary(DateTime startDate, DateTime endDate, [Service] ISummaryService queryService)
    {
        return queryService.GetSummary(startDate, endDate);
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

    public async Task<Quote> GetQuoteAsync(string symbol, [Service] IQuoteService queryService, CancellationToken cancellationToken = default)
    {
        return await queryService.GetQuoteAsync(symbol, cancellationToken);
    }

    public async Task<ExchangeRate> GetExchangeRateAsync(DateTime date, [Service] IExchangeRateService queryService, string? fromCurrencyCode = CurrencyCode.USD, string? toCurrencyCode = CurrencyCode.CRC, CancellationToken cancellationToken = default)
    {
        return await queryService.GetExchangeRateAsync(date, fromCurrencyCode, toCurrencyCode, cancellationToken);
    }
}