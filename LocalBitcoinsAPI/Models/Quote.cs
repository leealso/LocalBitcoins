using LocalBitcoinsAPI.Models.CoinMarketCap;

namespace LocalBitcoinsAPI.Models;

public class Quote
{
    public string Symbol { get; set; }

    public decimal Price { get;set; }

    public decimal PercentChange24h { get; set; }

    public decimal PercentChange7d { get; set; }

    public decimal PercentChange30d { get; set; }

    public decimal PercentChange90d { get; set; }

    public DateTime LastUpdated { get;set; }
    
    public Quote(CryptoCurrency cryptoCurrency)
    {
        Symbol = cryptoCurrency.Symbol;
        PercentChange24h = cryptoCurrency.Quote.Usd.PercentChange24h;
        PercentChange7d = cryptoCurrency.Quote.Usd.PercentChange7d;
        PercentChange30d = cryptoCurrency.Quote.Usd.PercentChange30d;
        PercentChange90d = cryptoCurrency.Quote.Usd.PercentChange90d;
        Price = cryptoCurrency.Quote.Usd.Price;
        LastUpdated = cryptoCurrency.Quote.Usd.LastUpdated;
    }
}

