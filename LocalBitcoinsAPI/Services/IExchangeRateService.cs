using LocalBitcoinsAPI.Models;

namespace LocalBitcoinsAPI.Services;

public interface IExchangeRateService
{
    Task<ExchangeRate> AddAsync(string fromCurrencyCode, string toCurrencyCode, DateTime date, decimal value, CancellationToken cancellationToken = default);
}
