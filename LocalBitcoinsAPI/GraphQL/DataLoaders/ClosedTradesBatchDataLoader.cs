using System.Linq;
using HotChocolate.Fetching;
using LocalBitcoinsAPI.Models;
using LocalBitcoinsAPI.Services;

namespace LocalBitcoinsAPI.GraphQL.DataLoaders;

public class ClosedTradesBatchDataLoader : BatchDataLoader<int, ClosedTrade>
{
	private readonly IClosedTradeService _queryService;

	public ClosedTradesBatchDataLoader(IClosedTradeService queryService, BatchScheduler batchScheduler) : base(batchScheduler)
	{
	  _queryService = queryService;
	}

	protected override async Task<IReadOnlyDictionary<int, ClosedTrade>> LoadBatchAsync(IReadOnlyList<int> keys, CancellationToken cancellationToken)
	{
        var closedTrades = await _queryService.GetAsync(keys, cancellationToken);
        return closedTrades.ToDictionary(x => x.ContactId, x => x);
    }
}