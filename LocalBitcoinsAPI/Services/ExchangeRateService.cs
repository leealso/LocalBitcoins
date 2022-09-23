using LocalBitcoinsAPI.Infrastructure.Data;
using LocalBitcoinsAPI.Models;
using LocalBitcoinsAPI.Utilities;
using Microsoft.EntityFrameworkCore;

namespace LocalBitcoinsAPI.Services;

public class ExchangeRateService : IExchangeRateService, IAsyncDisposable
{
    private readonly LocalBitcoinsDbContext _dbContext;

    public ExchangeRateService(IDbContextFactory<LocalBitcoinsDbContext> dbContextFactory)
    {
        _dbContext = dbContextFactory.CreateDbContext();
    }

    public async Task<ExchangeRate> AddAsync(string fromCurrencyCode, string toCurrencyCode, DateTime date, decimal value, CancellationToken cancellationToken = default)
    {
        var fromCurrency = await _dbContext.Currencies.SingleOrDefaultAsync(x => x.Code == fromCurrencyCode.ToUpper(), cancellationToken);
        if (fromCurrencyCode == null)
            throw QueryExceptionUtility.NotFoundException($"Currency {fromCurrencyCode} could not be found");
        
        var toCurrency = await _dbContext.Currencies.SingleOrDefaultAsync(x => x.Code == toCurrencyCode.ToUpper(), cancellationToken);
        if (toCurrency == null)
            throw QueryExceptionUtility.NotFoundException($"Currency {toCurrencyCode} could not be found");

        var exchangeRate = new ExchangeRate
        {
            FromCurrencyId = fromCurrency.Id,
            ToCurrencyId = toCurrency.Id,
            Date = date,
            Value = value
        };

        var result = await _dbContext.AddAsync(exchangeRate, cancellationToken);
        return result.Entity;
    }

    public ValueTask DisposeAsync()
    {
        return _dbContext.DisposeAsync();
    }
}
