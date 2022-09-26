using System.Linq;
using LocalBitcoinsAPI.Infrastructure.Data;
using LocalBitcoinsAPI.Infrastructure.HttpClients;
using LocalBitcoinsAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LocalBitcoinsAPI.Services;

public class AdvertisementService : IAdvertisementService
{
    private readonly ILocalBitcoinsHttpClient _httpClient;

    public AdvertisementService(ILocalBitcoinsHttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IQueryable<Advertisement>> GetBuyAdvertisementsAsync(string countryCode, CancellationToken cancellationToken = default)
    {
        var advertisements = await _httpClient.GetBuyAdsAsync(countryCode, cancellationToken);
        return advertisements.Select(x => new Advertisement(x)).AsQueryable();
    }

    public async Task<IQueryable<Advertisement>> GetSellAdvertisementsAsync(string countryCode, CancellationToken cancellationToken = default)
    {
        var advertisements = await _httpClient.GetSellAdsAsync(countryCode, cancellationToken);
        return advertisements.Select(x => new Advertisement(x)).AsQueryable();
    }
}
