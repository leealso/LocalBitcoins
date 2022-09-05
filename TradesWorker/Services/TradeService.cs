using TradesWorker.Constants;
using TradesWorker.Extensions;
using TradesWorker.HttpClients;
using TradesWorker.Infrastructure.Data;
using TradesWorker.Models;

namespace TradesWorker.Services;

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

    public async Task<IList<Trade>> UpdateTradesAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation($"Updating LocalBitcoins trades at {DateTime.Now}");
        
        var trades = new List<Trade>(); 
        var localBitcoinsTrades = await _localBitcoinsHttpClient.GetTradesAsync(cancellationToken);
        var addedTrades = await AddAsync(localBitcoinsTrades, cancellationToken);
        trades.AddRange(addedTrades);
        _logger.LogInformation($"Successfully updated LocalBitcoins trades at {DateTime.Now}");
        return trades;
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

    private async Task<IList<Trade>> AddAsync(IList<LocalBitcoinsTrade> localBitcoinsTrades, CancellationToken cancellationToken = default)
    {
        var trades = localBitcoinsTrades.Select(x => new Trade(x));
        var asyncTrades = trades.ToAsyncEnumerable();

        await foreach(var trade in asyncTrades)
        {
            await AddAsync(trade, cancellationToken);
        }

        await _dbContext.SaveChangesAsync(cancellationToken);

        return trades.ToList();
    }

    private async Task<Trade> AddAsync(Trade trade, CancellationToken cancellationToken = default)
    {
        if (_dbContext.Trades.Any(x => x.TransactionId == trade.TransactionId))
            return trade;
        
        var result = await _dbContext.Trades.AddAsync(trade, cancellationToken);
        return result.Entity;
    }

    private int GetMinTransactionId() 
    {
        return _dbContext.Trades.Any()
            ? _dbContext.Trades.Max(x => x.TransactionId)
            : Default.MaxTransactionId;
    }
}
