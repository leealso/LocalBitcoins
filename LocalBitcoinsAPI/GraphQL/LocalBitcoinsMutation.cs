using LocalBitcoinsAPI.Models;
using LocalBitcoinsAPI.Services;

namespace LocalBitcoinsAPI.GraphQL;

public class LocalBitcoinsMutation
{
    public async Task<ClosedTrade> AddClosedTradeAsync(ClosedTrade closedTrade, [Service] IClosedTradeService mutationService, CancellationToken cancellationToken = default)
    {
        return await mutationService.AddAsync(closedTrade, cancellationToken);
    }

    public async Task<IList<ClosedTrade>> AddClosedTradesAsync(IList<ClosedTrade> closedTrades, [Service] IClosedTradeService mutationService, CancellationToken cancellationToken = default)
    {
        return await mutationService.AddAsync(closedTrades, cancellationToken);
    }

    public async Task<ExchangeRate> AddExchangeRateAsync(string fromCurrencyCode, string toCurrencyCode, DateTime date, decimal value, [Service] IExchangeRateService mutationService, CancellationToken cancellationToken = default)
    {
        return await mutationService.AddAsync(fromCurrencyCode, toCurrencyCode, date, value, cancellationToken);
    }
}