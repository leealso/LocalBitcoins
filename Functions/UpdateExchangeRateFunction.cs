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
        public async Task Run([TimerTrigger("0 42 13 * * *")]TimerInfo myTimer, ILogger log, CancellationToken cancellationToken = default)
        {
            try
            {
                var startDate = new DateTime(2022, 01, 01);
                var endDate = new DateTime(2022, 12, 12);
                await _exchangeRateService.UpdateExchangeRatesAsync(startDate, endDate, cancellationToken);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine(ex.Message);
                _logger.LogError(ex, ex.Message);
            }
        }
    }
}
