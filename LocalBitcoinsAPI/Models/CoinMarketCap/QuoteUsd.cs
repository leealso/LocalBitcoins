using Newtonsoft.Json;

namespace LocalBitcoinsAPI.Models.CoinMarketCap;

public class QuoteUsd
{
    public decimal Price { get; set; }

    [JsonProperty("percent_change_24h")]
    public decimal PercentChange24h { get; set; }

    [JsonProperty("percent_change_7d")]
    public decimal PercentChange7d { get; set; }

    [JsonProperty("percent_change_30d")]
    public decimal PercentChange30d { get; set; }

    [JsonProperty("percent_change_90d")]
    public decimal PercentChange90d { get; set; }

    [JsonProperty("last_updated")]
    public DateTime LastUpdated { get; set; }
    
    public QuoteUsd()
    {
        
    }
}

