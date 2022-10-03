using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LocalBitcoins.Functions.Constants;
using LocalBitcoins.Functions.Infrastructure.HttpClients;
using LocalBitcoins.Functions.Models;
using Microsoft.Extensions.Logging;

namespace LocalBitcoins.Functions.Services;

public class ExchangeRateService : IExchangeRateService
{
    private readonly ILocalBitcoinsApiGraphClient _localBitcoinsApiGraphClient;

    private readonly IBccrHttpClient _bccrHttpClient;

    private readonly ILogger<ExchangeRateService> _logger;

    public ExchangeRateService(ILocalBitcoinsApiGraphClient localBitcoinsApiGraphClient, IBccrHttpClient bccrHttpClient, ILogger<ExchangeRateService> logger)
    {
        _localBitcoinsApiGraphClient = localBitcoinsApiGraphClient;
        _bccrHttpClient = bccrHttpClient;
        _logger = logger;
    }

    public async Task UpdateExchangeRateAsync(CancellationToken cancellationToken = default)
    {
        var date = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time"));
        var indicators = await _bccrHttpClient.GetExchangeRateAsync(date, cancellationToken);
        var indicator = indicators.FirstOrDefault();
        if (indicator == null)
            return;
        
        await AddExchangeRateAsync(indicator, cancellationToken);
    }

    public async Task UpdateExchangeRatesAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default)
    {
        var localStartDate = TimeZoneInfo.ConvertTimeFromUtc(startDate, TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time"));
        var localEndDate = TimeZoneInfo.ConvertTimeFromUtc(endDate, TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time"));
        var indicators = await _bccrHttpClient.GetExchangeRateAsync(localStartDate, localEndDate, cancellationToken);

        foreach (var indicator in indicators)
        {
            await AddExchangeRateAsync(indicator, cancellationToken);
        }  
    }

    private async Task<ExchangeRate> AddExchangeRateAsync(BccrIndicator indicator, CancellationToken cancellationToken = default)
    {
        return await _localBitcoinsApiGraphClient.MutationAsync<ExchangeRate>(GraphQlMutation.AddExchangeRate, new {
            FromCurrencyCode = CurrencyCode.USD,
            ToCurrencyCode = CurrencyCode.CRC,
            Date = indicator.Date,
            Value = indicator.Value
        }, cancellationToken);  
    }
}
