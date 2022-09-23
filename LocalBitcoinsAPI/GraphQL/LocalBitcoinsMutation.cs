using LocalBitcoinsAPI.Models;
using LocalBitcoinsAPI.Services;

namespace LocalBitcoinsAPI.GraphQL;

public class LocalBitcoinsMutation
{
    public async Task<ExchangeRate> AddExchangeRateAsync(string fromCurrencyCode, string toCurrencyCode, DateTime date, decimal value, [Service] IExchangeRateService mutationService, CancellationToken cancellationToken = default)
    {
        return await mutationService.AddAsync(fromCurrencyCode, toCurrencyCode, date, value, cancellationToken);
    }
}