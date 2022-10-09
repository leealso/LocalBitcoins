using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LocalBitcoins.Functions.Constants;
using LocalBitcoins.Functions.Infrastructure.Data;
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
        
        var localBitcoinsTrades = await _localBitcoinsHttpClient.GetClosedTradesAsync(cancellationToken);
        localBitcoinsTrades = localBitcoinsTrades.Where(x => x.PaymentCompletedAt.HasValue).ToList();

        if (localBitcoinsTrades.Any())
        {
            var addedTrades = await AddAsync(localBitcoinsTrades, cancellationToken);
            _logger.LogInformation($"Successfully added {addedTrades.Count} new LocalBitcoins closed trades at {DateTime.Now}");
        }
        else 
        {
            _logger.LogInformation($"There were no new LocalBitcoins closed trades at {DateTime.Now}");
        }        
    }

    private async Task<IList<ClosedTrade>> AddAsync(IList<LocalBitcoinsContactData> localBitcoinsTrades, CancellationToken cancellationToken = default)
    {
        var closedAt = localBitcoinsTrades.Min(x => x.ClosedAt);
        var contactIds = localBitcoinsTrades.Select(x => x.ContactId);

        var missingContactIds = await _localBitcoinsApiGraphClient.QueryAsync<IList<int>>(GraphQlQuery.MissingContactIds, new {
            closedAt,
            contactIds
        }, cancellationToken);

        if (!missingContactIds.Any())
            return Array.Empty<ClosedTrade>();

        var closedTrades = localBitcoinsTrades
            .Where(x => missingContactIds.Contains(x.ContactId))
            .Select(x => new ClosedTrade(x));

        var addedTrades = await _localBitcoinsApiGraphClient.MutationAsync<IList<ClosedTrade>>(GraphQlMutation.AddClosedTrades, new {
            closedTrades = closedTrades
        });
        
        return addedTrades;
    }
}
