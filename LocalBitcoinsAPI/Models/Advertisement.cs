using LocalBitcoinsAPI.Constants;
using LocalBitcoinsAdvertisement = LocalBitcoinsAPI.Models.LocalBitcoins.Advertisement;

namespace LocalBitcoinsAPI.Models;

public class Advertisement
{
    public string Username { get; set; }

    public string Currency { get; set; }

    public decimal TempPrice { get; set; }

    public decimal TempPriceUsd { get; set; }

    public string PublicViewUrl { get; set; }

    public Advertisement(LocalBitcoinsAdvertisement localBitcoinsAdvertisement, ExchangeRate exchangeRate)
    {
        Username = localBitcoinsAdvertisement.Data.Profile.Username;
        Currency = localBitcoinsAdvertisement.Data.Currency;
        var isUsd = CurrencyCode.IsUsd(Currency);
        TempPrice = isUsd 
            ? Math.Round(decimal.Parse(localBitcoinsAdvertisement.Data.TempPrice) * exchangeRate.Value, 2)
            : decimal.Parse(localBitcoinsAdvertisement.Data.TempPrice);
        TempPriceUsd = isUsd 
            ? decimal.Parse(localBitcoinsAdvertisement.Data.TempPriceUsd)
            : Math.Round(decimal.Parse(localBitcoinsAdvertisement.Data.TempPrice) / exchangeRate.Value, 2);
        PublicViewUrl = localBitcoinsAdvertisement.Actions.PublicView;
    }
}