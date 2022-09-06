using Quartz;
using TradesWorker.Services;

namespace TradesWorker.Infrastructure.CronJobs;

public class UpdateTradesJob : IJob
{
    private readonly ITradeService _tradeService;

    public UpdateTradesJob(ITradeService tradeService)
    {
        _tradeService = tradeService;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        await _tradeService.UpdateTradesAsync(context.CancellationToken);
    }
}