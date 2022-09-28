using LocalBitcoinsAPI.Models.CoinMarketCap;

namespace LocalBitcoinsAPI.Infrastructure.HttpClients;

public interface ICoinMarketCapHttpClient
{
    Task<CryptoCurrency> GetQuoteAsync(string symbol = "BTC", CancellationToken cancellationToken = default);
}
