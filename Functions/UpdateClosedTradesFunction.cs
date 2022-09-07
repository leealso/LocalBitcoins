using System.Threading;
using System.Threading.Tasks;
using LocalBitcoins.Functions.Services;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace LocalBitcoins.Functions
{
    public class UpdateClosedTradesFunction
    {
        private readonly IClosedTradeService _closedTradeService;

        public UpdateClosedTradesFunction(IClosedTradeService closedTradeService)
        {
            _closedTradeService = closedTradeService;
        }
        
        [FunctionName("UpdateTradesFunction")]
        public async Task Run([TimerTrigger("0 */15 * * * *")]TimerInfo myTimer, ILogger log, CancellationToken cancellationToken = default)
        {
            await _closedTradeService.UpdateClosedTradesAsync(cancellationToken);
        }
    }
}
