using LocalBitcoinsAPI.Infrastructure.HttpClients;
using LocalBitcoinsAPI.Models;

namespace LocalBitcoinsAPI.Services;

public class AdvertisementService : IAdvertisementService
{
    private readonly IExchangeRateService _exchangeRateService;

    private readonly ILocalBitcoinsHttpClient _httpClient;

    public AdvertisementService(IExchangeRateService exchangeRateService, ILocalBitcoinsHttpClient httpClient)
    {
        _exchangeRateService = exchangeRateService;
        _httpClient = httpClient;
    }

    public async Task<IQueryable<Advertisement>> GetBuyAdvertisementsAsync(string countryCode, CancellationToken cancellationToken = default)
    {
        var advertisements = await _httpClient.GetBuyAdsAsync(countryCode, cancellationToken);
        var date = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow.Date, TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time"));
        var exchangeRate = await _exchangeRateService.GetExchangeRateAsync(date.Date, cancellationToken);
        return advertisements.Select(x => new Advertisement(x, exchangeRate))
            .Where(IsAdvertisementValid)
            .AsQueryable();
    }

    public async Task<IQueryable<Advertisement>> GetSellAdvertisementsAsync(string countryCode, CancellationToken cancellationToken = default)
    {
        var advertisements = await _httpClient.GetSellAdsAsync(countryCode, cancellationToken);
        var date = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow.Date, TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time"));
        var exchangeRate = await _exchangeRateService.GetExchangeRateAsync(date.Date, cancellationToken);
        return advertisements.Select(x => new Advertisement(x, exchangeRate))
            .Where(IsAdvertisementValid)
            .AsQueryable();
    }

    private bool IsAdvertisementValid(Advertisement advertisement)
    {
        return advertisement.MaxAmountAvailable >= advertisement.MinAmountAvailable;
    }
}
