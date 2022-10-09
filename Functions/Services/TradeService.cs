using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LocalBitcoins.Functions.Constants;
using LocalBitcoins.Functions.Infrastructure.HttpClients;
using LocalBitcoins.Functions.Models;
using LocalBitcoins.Functions.Utilities;
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
        var maxTrade = await _localBitcoinsApiGraphClient.QueryAsync<GraphQlPagination<Trade>>(GraphQlQuery.GetMaxTransactionId);
        int maxTransactionId = maxTrade.Items.Any() 
            ? maxTrade.Items.Select(x => x.TransactionId).First()
            : default;

        var trades = localBitcoinsTrades
            .Where(x => x.TId > maxTransactionId)
            .Select(x => new Trade(x));

        if (!trades.Any())
            return Array.Empty<Trade>();

        var addedTrades = await _localBitcoinsApiGraphClient.MutationAsync<IList<Trade>>(GraphQlMutation.AddTrades, new {
            trades = trades
        });
        
        return addedTrades;
    }

    public async Task UpdateTradesAsync(DateTime startDate, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation($"Updating LocalBitcoins trades from {startDate.Date} to {DateTime.Now}");

        var completed = false;
        do 
        {
            var minTrade = await _localBitcoinsApiGraphClient.QueryAsync<GraphQlPagination<Trade>>(GraphQlQuery.GetMinTransactionId);
            int minTransactionId = minTrade.Items.Any() 
                ? minTrade.Items.Select(x => x.TransactionId).First()
                : default;

            var localBitcoinsTrades = await _localBitcoinsHttpClient.GetTradesAsync(minTransactionId, cancellationToken);
            localBitcoinsTrades = localBitcoinsTrades.Where(x => DateTimeUtility.FromEpoch(x.Date) >= startDate.Date)
                .ToList();
            
            if (!localBitcoinsTrades.Any())
                completed = true;
            else
            {
                var addedTrades = await _localBitcoinsApiGraphClient.MutationAsync<IList<Trade>>(GraphQlMutation.AddTrades, new {
                    trades = localBitcoinsTrades.Select(x => new Trade(x))
                });
                if (addedTrades.Any())
                    _logger.LogInformation($"Successfully added {addedTrades.Count} new LocalBitcoins trades at {DateTime.Now}");
                else 
                    _logger.LogInformation($"There were no new LocalBitcoins trades at {DateTime.Now}");
            }
        } while (!completed);
    }
}
