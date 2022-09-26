using LocalBitcoinsAPI.Models;

namespace LocalBitcoinsAPI.Services;

public interface IAdvertisementService
{
    Task<IQueryable<Advertisement>> GetBuyAdvertisementsAsync(string countryCode, CancellationToken cancellationToken = default);

    Task<IQueryable<Advertisement>> GetSellAdvertisementsAsync(string countryCode, CancellationToken cancellationToken = default);
}
