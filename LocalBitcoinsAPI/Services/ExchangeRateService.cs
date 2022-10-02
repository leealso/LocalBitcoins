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

        var currentExchangeRate = await _dbContext.ExchanteRates
            .SingleOrDefaultAsync(x => x.FromCurrencyId == fromCurrency.Id && x.ToCurrencyId == toCurrency.Id && x.Date == date, cancellationToken);

        if (currentExchangeRate == null)
            return await AddAsync(fromCurrency.Id, toCurrency.Id, date, value, cancellationToken);
        
        currentExchangeRate.Value = value; 
        await _dbContext.SaveChangesAsync(cancellationToken);
        return currentExchangeRate;
    }

    private async Task<ExchangeRate> AddAsync(int fromCurrencyId, int toCurrencyId, DateTime date, decimal value, CancellationToken cancellationToken = default)
    {
        var exchangeRate = new ExchangeRate
        {
            FromCurrencyId = fromCurrencyId,
            ToCurrencyId = toCurrencyId,
            Date = date,
            Value = value
        };

        var result = await _dbContext.AddAsync(exchangeRate, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return result.Entity;
    }

    public ValueTask DisposeAsync()
    {
        return _dbContext.DisposeAsync();
    }
}
