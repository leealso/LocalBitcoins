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

public class ClosedTradeService : IClosedTradeService
{
    private readonly ILocalBitcoinsHttpClient _localBitcoinsHttpClient;

    private readonly ILocalBitcoinsApiGraphClient _localBitcoinsApiGraphClient;

    private readonly ILogger<ClosedTradeService> _logger;

    public ClosedTradeService(ILocalBitcoinsHttpClient localBitcoinsHttpClient, 
        ILocalBitcoinsApiGraphClient localBitcoinsApiGraphClient, ILogger<ClosedTradeService> logger)
    {
        _localBitcoinsHttpClient = localBitcoinsHttpClient;
        _localBitcoinsApiGraphClient = localBitcoinsApiGraphClient;
        _logger = logger;
    }

    public async Task UpdateClosedTradesAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation($"Updating LocalBitcoins closed trades at {DateTime.Now}");
        
        var localBitcoinsTrades = await _localBitcoinsHttpClient.GetReleasedTradesAsync(cancellationToken);
        
        var addedTrades = await AddAsync(localBitcoinsTrades, cancellationToken);
        if (addedTrades.Any())
            _logger.LogInformation($"Successfully added {addedTrades.Count} new LocalBitcoins closed trades at {DateTime.Now}");
        else 
            _logger.LogInformation($"There were no new LocalBitcoins closed trades at {DateTime.Now}");
    }

    private async Task<IList<ClosedTrade>> AddAsync(IList<LocalBitcoinsContactData> localBitcoinsTrades, CancellationToken cancellationToken = default)
    {
        var todaysClosedTrades = await _localBitcoinsApiGraphClient.QueryAsync<GraphQlPagination<ClosedTrade>>(GraphQlQuery.GetClosedTrades, cancellationToken);
        var contactIds = todaysClosedTrades.Items.Select(x => x.ContactId);

        var closedTrades = localBitcoinsTrades
            .Where(x => !contactIds.Contains(x.ContactId))
            .Select(x => new ClosedTrade(x));

        if (!closedTrades.Any())
            return Array.Empty<ClosedTrade>();

        var addedTrades = await _localBitcoinsApiGraphClient.MutationAsync<IList<ClosedTrade>>(GraphQlMutation.AddClosedTrades, new {
            closedTrades = closedTrades
        }, cancellationToken);
        
        return addedTrades;
    }

    public async Task UpdateClosedTradesAsync(DateTime startDate, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation($"Updating LocalBitcoins closed trades from {startDate.Date} to {DateTime.Now}");

        var completed = false;
        DateTime? closedAt = null;
        do 
        {
            var localBitcoinsTrades = await _localBitcoinsHttpClient.GetReleasedTradesAsync(closedAt, cancellationToken);
            localBitcoinsTrades = localBitcoinsTrades.Where(x => x.ClosedAt >= startDate.Date)
                .ToList();
            
            if (!localBitcoinsTrades.Any())
                completed = true;
            else
            {
                closedAt = localBitcoinsTrades.Min(x => x.ClosedAt);
                var addedClosedTrades = await _localBitcoinsApiGraphClient.MutationAsync<IList<Trade>>(GraphQlMutation.AddClosedTrades, new {
                    closedTrades = localBitcoinsTrades.Select(x => new ClosedTrade(x))
                });
                if (addedClosedTrades.Any())
                    _logger.LogInformation($"Successfully added {addedClosedTrades.Count} new LocalBitcoins closed trades at {DateTime.Now}");
                else 
                    _logger.LogInformation($"There were no new LocalBitcoins closed trades at {DateTime.Now}");
            }
        } while (!completed);
    }
}
