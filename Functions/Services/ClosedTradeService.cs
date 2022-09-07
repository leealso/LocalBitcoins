using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LocalBitcoins.Functions.Extensions;
using LocalBitcoins.Functions.Infrastructure.Data;
using LocalBitcoins.Functions.Infrastructure.HttpClients;
using LocalBitcoins.Functions.Models;
using Microsoft.Extensions.Logging;

namespace LocalBitcoins.Functions.Services;

public class ClosedTradeService : IClosedTradeService
{
    private readonly LocalBitcoinsDbContext _dbContext;

    private readonly ILocalBitcoinsHttpClient _localBitcoinsHttpClient;

    private readonly ILogger<ClosedTradeService> _logger;

    public ClosedTradeService(LocalBitcoinsDbContext dbContext, ILocalBitcoinsHttpClient localBitcoinsHttpClient, ILogger<ClosedTradeService> logger)
    {
        _dbContext = dbContext;
        _localBitcoinsHttpClient = localBitcoinsHttpClient;
        _logger = logger;
    }

    public async Task UpdateClosedTradesAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation($"Updating LocalBitcoins closed trades at {DateTime.Now}");
        
        var localBitcoinsTrades = await _localBitcoinsHttpClient.GetClosedTradesAsync(cancellationToken);

        if (localBitcoinsTrades.Any())
        {
            var addedTrades = await AddAsync(localBitcoinsTrades.ToList(), cancellationToken);
            _logger.LogInformation($"Successfully added {addedTrades.Count} new LocalBitcoins closed trades at {DateTime.Now}");
        }
        else 
        {
            _logger.LogInformation($"There were no new LocalBitcoins closed trades at {DateTime.Now}");
        }        
    }

    private async Task<IList<ClosedTrade>> AddAsync(IList<LocalBitcoinsContact> localBitcoinsTrades, CancellationToken cancellationToken = default)
    {
        var addedTrades = new List<ClosedTrade>();
        var asyncTrades = localBitcoinsTrades.Select(x => new ClosedTrade(x)).ToAsyncEnumerable();

        await foreach(var trade in asyncTrades)
        {
            if (!_dbContext.ClosedTrades.Any(x => x.ContactId == trade.ContactId))
            {
                var result = await _dbContext.ClosedTrades.AddAsync(trade, cancellationToken);
                addedTrades.Add(result.Entity);
            }
        }

        if (addedTrades.Count > 0)
            await _dbContext.SaveChangesAsync(cancellationToken);

        return addedTrades;
    }
}
