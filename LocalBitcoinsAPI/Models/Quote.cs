using LocalBitcoinsAPI.Models.CoinMarketCap;

namespace LocalBitcoinsAPI.Models;

public class Quote
{
    public string Symbol { get; set; }

    public decimal Price { get;set; }

    public DateTime LastUpdated { get;set; }
    
    public Quote(CryptoCurrency cryptoCurrency)
    {
        Symbol = cryptoCurrency.Symbol;
        Price = cryptoCurrency.Quote.Usd.Price;
        LastUpdated = cryptoCurrency.Quote.Usd.LastUpdated;
    }
}

