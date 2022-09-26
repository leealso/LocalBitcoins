using LocalBitcoinsAdvertisement = LocalBitcoinsAPI.Models.LocalBitcoins.Advertisement;

namespace LocalBitcoinsAPI.Models;

public class Advertisement
{
    public string Username { get; set; }

    public string Currency { get; set; }

    public decimal TempPrice { get; set; }

    public decimal TempPriceUsd { get; set; }

    public string PublicViewUrl { get; set; }

    public Advertisement(LocalBitcoinsAdvertisement localBitcoinsAdvertisement)
    {
        Username = localBitcoinsAdvertisement.Data.Profile.Username;
        Currency = localBitcoinsAdvertisement.Data.Currency;
        TempPrice = decimal.Parse(localBitcoinsAdvertisement.Data.TempPrice);
        TempPriceUsd = decimal.Parse(localBitcoinsAdvertisement.Data.TempPriceUsd);
        PublicViewUrl = localBitcoinsAdvertisement.Actions.PublicView;
    }
}