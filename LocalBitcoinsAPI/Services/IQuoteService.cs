using LocalBitcoinsAPI.Models;

namespace LocalBitcoinsAPI.Services;

public interface IQuoteService
{
    Task<Quote> GetQuoteAsync(string symbol = "BTC", CancellationToken cancellationToken = default);
}
