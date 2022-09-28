using Newtonsoft.Json;

namespace LocalBitcoinsAPI.Models.CoinMarketCap;

public class CryptoCurrency
{
    public string Symbol { get; set; }

    public Quote Quote { get; set; }
    
    public CryptoCurrency()
    {
        
    }
}