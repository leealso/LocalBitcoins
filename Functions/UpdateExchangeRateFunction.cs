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

        public UpdateExchangeRateFunction(IExchangeRateService exchangeRateService)
        {
            _exchangeRateService = exchangeRateService;
        }
        
        [FunctionName("UpdateExchangeRateFunction")]
        public async Task Run([TimerTrigger("0 0 0,12,18,19 * * *")]TimerInfo myTimer, ILogger log, CancellationToken cancellationToken = default)
        {
            var startDate = new DateTime(2022, 01, 01);
            var endDate = new DateTime(2022, 12, 12);
            await _exchangeRateService.UpdateExchangeRatesAsync(startDate, endDate, cancellationToken);
        }
    }
}
