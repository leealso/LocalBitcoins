using LocalBitcoinsAPI.Infrastructure.Data;
using LocalBitcoinsAPI.Models;
using LocalBitcoinsAPI.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace LocalBitcoinsAPI.Services;

public class ExchangeRateService : IExchangeRateService, IAsyncDisposable
{
    private readonly LocalBitcoinsDbContext _dbContext;

    private readonly IMemoryCache _memoryCache;

    private readonly int _absoluteExpirationInMinutes;
    
    private readonly int _slidingExpirationInMinutes;

    public ExchangeRateService(IDbContextFactory<LocalBitcoinsDbContext> dbContextFactory, IMemoryCache memoryCache, IConfiguration configuration)
    {
        _dbContext = dbContextFactory.CreateDbContext();
        _memoryCache = memoryCache;
        _absoluteExpirationInMinutes = configuration.GetValue<int>("MemoryCache:AbsoluteExpirationMinutes");
        _slidingExpirationInMinutes = configuration.GetValue<int>("MemoryCache:SlidingExpirationMinutes");
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

    public async Task<ExchangeRate> GetExchangeRateAsync(DateTime date, string fromCurrencyCode, string toCurrencyCode, CancellationToken cancellationToken = default)
    {
        return await _memoryCache.GetOrCreateAsync<ExchangeRate>($"{fromCurrencyCode}_{toCurrencyCode}_{date.Date}", async entry => 
        {
            entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(_absoluteExpirationInMinutes));
            entry.SetSlidingExpiration(TimeSpan.FromMinutes(_slidingExpirationInMinutes));
            return await GetExchangeRateFromSourceAsync(date, fromCurrencyCode, toCurrencyCode, cancellationToken);
        });
    }

    private async Task<ExchangeRate> GetExchangeRateFromSourceAsync(DateTime date, string fromCurrencyCode, string toCurrencyCode, CancellationToken cancellationToken = default)
    {
        var fromCurrency = await _dbContext.Currencies.SingleOrDefaultAsync(x => x.Code == fromCurrencyCode.ToUpper(), cancellationToken);
        if (fromCurrencyCode == null)
            throw QueryExceptionUtility.NotFoundException($"Currency {fromCurrencyCode} could not be found");
        
        var toCurrency = await _dbContext.Currencies.SingleOrDefaultAsync(x => x.Code == toCurrencyCode.ToUpper(), cancellationToken);
        if (toCurrency == null)
            throw QueryExceptionUtility.NotFoundException($"Currency {toCurrencyCode} could not be found");
        
        var exchangeRate = await _dbContext.ExchanteRates
            .Where(x => x.FromCurrencyId == fromCurrency.Id && x.ToCurrencyId == toCurrency.Id && x.Date >= date)
            .OrderBy(x => x.Id)
            .FirstOrDefaultAsync(cancellationToken);

        if (exchangeRate == null)
            throw QueryExceptionUtility.NotFoundException($"Exchange rate from {fromCurrencyCode} to {toCurrencyCode} for {date} could not be found");
        return exchangeRate;
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
