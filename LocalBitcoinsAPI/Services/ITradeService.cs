using LocalBitcoinsAPI.Models;

public interface ITradeService
{
    IQueryable<Trade> GetTrades();
}
