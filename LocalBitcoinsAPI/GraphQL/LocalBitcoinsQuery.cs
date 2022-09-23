using LocalBitcoinsAPI.Models;
using LocalBitcoinsAPI.Services;

namespace LocalBitcoinsAPI.GraphQL;

public class LocalBitcoinsQuery
{
    [UseOffsetPaging(MaxPageSize = 100, IncludeTotalCount = true)]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Trade> GetTrades([Service] ITradeService queryService)
    {
        return queryService.GetTrades();
    }


    [UseOffsetPaging(MaxPageSize = 100, IncludeTotalCount = true)]
    [UseFiltering]
    [UseSorting]
    public IQueryable<ClosedTrade> GetClosedTrades([Service] IClosedTradeService queryService)
    {
        return queryService.GetClosedTrades();
    }
}