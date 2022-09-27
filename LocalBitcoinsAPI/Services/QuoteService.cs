using LocalBitcoinsAPI.Infrastructure.Data;
using LocalBitcoinsAPI.Infrastructure.HttpClients;
using LocalBitcoinsAPI.Models;
using LocalBitcoinsAPI.Utilities;
namespace LocalBitcoinsAPI.Services;

public class QuoteService : IQuoteService
{
    private readonly ICoinMarketCapHttpClient _httpClient;

    public QuoteService(ICoinMarketCapHttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<Quote> GetQuoteAsync(string symbol = "BTC", CancellationToken cancellationToken = default)
    {
        var quote = await _httpClient.GetQuoteAsync(symbol, cancellationToken);
        return new Quote(quote);
    }
}
