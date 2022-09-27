using Newtonsoft.Json;

namespace LocalBitcoinsAPI.Models.CoinMarketCap;

public class Quote
{
    [JsonProperty("USD")]
    public QuoteUsd Usd { get; set; }
    
    public Quote()
    {
        
    }
}

