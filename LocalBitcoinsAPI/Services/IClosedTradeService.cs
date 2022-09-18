using LocalBitcoinsAPI.Models;

public interface IClosedTradeService
{
    IQueryable<ClosedTrade> GetClosedTrades();
}
