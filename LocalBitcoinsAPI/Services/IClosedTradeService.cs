using LocalBitcoinsAPI.Models;

namespace LocalBitcoinsAPI.Services;

public interface IClosedTradeService
{
    IQueryable<ClosedTrade> GetClosedTrades();
}
