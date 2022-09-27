using Newtonsoft.Json;

namespace LocalBitcoinsAPI.Models.CoinMarketCap;

public class QuoteUsd
{
    public decimal Price { get; set; }

    [JsonProperty("last_updated")]
    public DateTime LastUpdated { get; set; }
    
    public QuoteUsd()
    {
        
    }
}

