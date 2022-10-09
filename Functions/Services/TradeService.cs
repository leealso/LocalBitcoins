using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LocalBitcoins.Functions.Constants;
using LocalBitcoins.Functions.Infrastructure.HttpClients;
using LocalBitcoins.Functions.Models;
using Microsoft.Extensions.Logging;

namespace LocalBitcoins.Functions.Services;

public class TradeService : ITradeService
{
    private readonly ILocalBitcoinsHttpClient _localBitcoinsHttpClient;

    private readonly ILocalBitcoinsApiGraphClient _localBitcoinsApiGraphClient;

    private readonly ILogger<TradeService> _logger;

    public TradeService(ILocalBitcoinsHttpClient localBitcoinsHttpClient, 
        ILocalBitcoinsApiGraphClient localBitcoinsApiGraphClient, ILogger<TradeService> logger)
    {
        _localBitcoinsHttpClient = localBitcoinsHttpClient;
        _localBitcoinsApiGraphClient = localBitcoinsApiGraphClient;
        _logger = logger;
    }

    public async Task UpdateTradesAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation($"Updating LocalBitcoins trades at {DateTime.Now}");
        
        var localBitcoinsTrades = await _localBitcoinsHttpClient.GetTradesAsync(cancellationToken);

        var addedTrades = await AddAsync(localBitcoinsTrades, cancellationToken);
        if (addedTrades.Any())
            _logger.LogInformation($"Successfully added {addedTrades.Count} new LocalBitcoins trades at {DateTime.Now}");
        else 
            _logger.LogInformation($"There were no new LocalBitcoins trades at {DateTime.Now}");
    }

    private async Task<IList<Trade>> AddAsync(IList<LocalBitcoinsTrade> localBitcoinsTrades, CancellationToken cancellationToken = default)
    {
        var latestTransactionId = await _localBitcoinsApiGraphClient.QueryAsync<int>(GraphQlQuery.GetLatestTransactionId, cancellationToken);

        var trades = localBitcoinsTrades
            .Where(x => x.TId > latestTransactionId)
            .Select(x => new Trade(x));

        if (!trades.Any())
            return Array.Empty<Trade>();

        var addedTrades = await _localBitcoinsApiGraphClient.MutationAsync<IList<Trade>>(GraphQlMutation.AddTrades, new {
            trades = trades
        });
        
        return addedTrades;
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
