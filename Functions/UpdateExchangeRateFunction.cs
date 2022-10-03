using System;
using System.Threading;
using System.Threading.Tasks;
using LocalBitcoins.Functions.Services;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace LocalBitcoins.Functions
{
    public class UpdateExchangeRateFunction
    {
        private readonly IExchangeRateService _exchangeRateService;

        private readonly ILogger<UpdateExchangeRateFunction> _logger;

        public UpdateExchangeRateFunction(IExchangeRateService exchangeRateService, ILogger<UpdateExchangeRateFunction> logger)
        {
            _exchangeRateService = exchangeRateService;
            _logger = logger;
        }
        
        [FunctionName("UpdateExchangeRateFunction")]
        public async Task Run([TimerTrigger("0 0 0,6,13,18,19 * * *")]TimerInfo myTimer, ILogger log, CancellationToken cancellationToken = default)
        {
            await _exchangeRateService.UpdateExchangeRateAsync(cancellationToken);
        }
    }
}
