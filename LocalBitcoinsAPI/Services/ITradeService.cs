using LocalBitcoinsAPI.Models;

namespace LocalBitcoinsAPI.Services;

public interface ITradeService
{
    IQueryable<Trade> GetTrades();
}
