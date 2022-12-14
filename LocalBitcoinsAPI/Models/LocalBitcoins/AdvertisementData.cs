using Newtonsoft.Json;

namespace LocalBitcoinsAPI.Models.LocalBitcoins;

public class AdvertisementData
{
    public Profile Profile { get; set; }

    public string Currency { get; set; }

    [JsonProperty("min_amount")]
    public decimal? MinAmount { get; set; }

    [JsonProperty("online_provider")]
    public string OnlineProvider { get; set; }

    [JsonProperty("min_amount_available")]
    public string MinAmountAvailable { get; set; }

    [JsonProperty("max_amount")]
    public string? MaxAmount { get; set; }

    [JsonProperty("max_amount_available")]
    public string MaxAmountAvailable { get; set; }

    [JsonProperty("temp_price_usd")]
    public string TempPriceUsd { get; set; }

    [JsonProperty("temp_price")]
    public string TempPrice { get; set; }

    [JsonProperty("bank_name")]
    public string BankName { get; set; }

    [JsonProperty("visible")]
    public bool Visible { get; set; }

    [JsonProperty("created_at")]
    public DateTime CreatedAt { get; set; }

    public AdvertisementData()
    {
        
    }
}