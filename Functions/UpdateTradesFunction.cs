using System.Threading;
using System.Threading.Tasks;
using LocalBitcoins.Functions.Services;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace LocalBitcoins.Functions
{
    public class UpdateTradesFunction
    {
        private readonly ITradeService _tradeService;

        public UpdateTradesFunction(ITradeService tradeService)
        {
            _tradeService = tradeService;
        }
        
        [FunctionName("UpdateTradesFunction")]
        public async Task Run([TimerTrigger("0 */15 * * * *")]TimerInfo myTimer, ILogger log, CancellationToken cancellationToken = default)
        {
            await _tradeService.UpdateTradesAsync(cancellationToken);
        }
    }
}
