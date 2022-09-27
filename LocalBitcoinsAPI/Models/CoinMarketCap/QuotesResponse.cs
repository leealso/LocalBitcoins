using Newtonsoft.Json;

namespace LocalBitcoinsAPI.Models.CoinMarketCap;

public class QuoteResponse
{
    [JsonProperty("USD")]
    public QuoteUsd Usd { get; set; }
    
    public QuoteResponse()
    {
        
    }
}

