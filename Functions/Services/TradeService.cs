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

public class TradeService : ITradeService
{
    private readonly LocalBitcoinsDbContext _dbContext;

    private readonly ILocalBitcoinsHttpClient _localBitcoinsHttpClient;

    private readonly ILogger<TradeService> _logger;

    public TradeService(LocalBitcoinsDbContext dbContext, ILocalBitcoinsHttpClient localBitcoinsHttpClient, ILogger<TradeService> logger)
    {
        _dbContext = dbContext;
        _localBitcoinsHttpClient = localBitcoinsHttpClient;
        _logger = logger;
    }

    public async Task UpdateTradesAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation($"Updating LocalBitcoins trades at {DateTime.Now}");
        
        var localBitcoinsTrades = await _localBitcoinsHttpClient.GetTradesAsync(cancellationToken);
        var latestTransactionId = GetLatestTransactionId();
        var newLocalBitcoinsTrades = localBitcoinsTrades.Where(x => x.TId > latestTransactionId);

        if (newLocalBitcoinsTrades.Any())
        {
            var addedTrades = await AddAsync(newLocalBitcoinsTrades.ToList(), cancellationToken);
            _logger.LogInformation($"Successfully added {addedTrades.Count} new LocalBitcoins trades at {DateTime.Now}");
        }
        else 
        {
            _logger.LogInformation($"There were no new LocalBitcoins trades at {DateTime.Now}");
        }        
    }

    private async Task<IList<Trade>> AddAsync(IList<LocalBitcoinsTrade> localBitcoinsTrades, CancellationToken cancellationToken = default)
    {
        var addedTrades = new List<Trade>();
        var asyncTrades = localBitcoinsTrades.Select(x => new Trade(x)).ToAsyncEnumerable();

        await foreach(var trade in asyncTrades)
        {
            if (!_dbContext.Trades.Any(x => x.TransactionId == trade.TransactionId))
            {
                var result = await _dbContext.Trades.AddAsync(trade, cancellationToken);
                addedTrades.Add(result.Entity);
            }
        }

        if (addedTrades.Count > 0)
            await _dbContext.SaveChangesAsync(cancellationToken);

        return addedTrades;
    }

    private int GetLatestTransactionId() 
    {
        return _dbContext.Trades.Any()
            ? _dbContext.Trades.Max(x => x.TransactionId)
            : 0;
    }

    /*public async Task<IList<Trade>> UpdateTradesAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation($"Updating LocalBitcoins trades at {DateTime.Now}");
        var trades = new List<Trade>(); 
        var latestTransactionId = 0;

        do
        {
            var localBitcoinsTrades = await _localBitcoinsHttpClient.GetTradesAsync(latestTransactionId, cancellationToken);
            latestTransactionId = localBitcoinsTrades.Min(x => x.TId);
            
            var addedTrades = await AddAsync(localBitcoinsTrades, cancellationToken);
            trades.AddRange(addedTrades);
        }
        while (latestTransactionId > Default.MaxTransactionId);

        _logger.LogInformation($"Successfully updated LocalBitcoins trades at {DateTime.Now} ({trades.Count})");
        return trades;
    }*/
}
