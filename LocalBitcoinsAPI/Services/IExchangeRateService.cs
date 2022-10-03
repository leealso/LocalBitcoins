using LocalBitcoinsAPI.Constants;
using LocalBitcoinsAPI.Models;

namespace LocalBitcoinsAPI.Services;

public interface IExchangeRateService
{
    Task<ExchangeRate> AddAsync(string fromCurrencyCode, string toCurrencyCode, DateTime date, decimal value, CancellationToken cancellationToken = default);

    Task<ExchangeRate> GetExchangeRateAsync(DateTime date, string fromCurrencyCode, string toCurrencyCode, CancellationToken cancellationToken = default);

    Task<ExchangeRate> GetExchangeRateAsync(DateTime date, CancellationToken cancellationToken = default) 
        => GetExchangeRateAsync(date, CurrencyCode.USD, CurrencyCode.CRC, cancellationToken);
}
