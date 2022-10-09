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
        var latestContactId = await _localBitcoinsApiGraphClient.QueryAsync<int>(GraphQlQuery.GetLatestContactId, cancellationToken);

        var closedTrades = localBitcoinsTrades
            .Where(x => x.ContactId > latestContactId)
            .Select(x => new ClosedTrade(x));

        if (!closedTrades.Any())
            return Array.Empty<ClosedTrade>();

        var addedTrades = await _localBitcoinsApiGraphClient.MutationAsync<IList<ClosedTrade>>(GraphQlMutation.AddClosedTrades, new {
            closedTrades = closedTrades
        });
        
        return addedTrades;
    }
}
