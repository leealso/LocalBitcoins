using TradesWorker.Services;

namespace TradesWorker;

public class Worker : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    private readonly ILogger<Worker> _logger;

    public Worker(IServiceProvider serviceProvider, ILogger<Worker> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var scope = _serviceProvider.CreateScope();
            var tradeService = scope.ServiceProvider.GetRequiredService<ITradeService>();
            await tradeService.UpdateTradesAsync(stoppingToken);

            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            await Task.Delay(60000, stoppingToken);
        }
    }
}
