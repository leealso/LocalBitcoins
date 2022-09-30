using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LocalBitcoins.Functions.Constants;
using LocalBitcoins.Functions.Infrastructure.HttpClients;
using LocalBitcoins.Functions.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace LocalBitcoins.Functions
{
    public class UpdateExchangeRateFunction
    {
        private readonly ILocalBitcoinsApiGraphClient _localBitcoinsApiGraphClient;

        private readonly IBccrHttpClient _bccrHttpClient;

        public UpdateExchangeRateFunction(ILocalBitcoinsApiGraphClient localBitcoinsApiGraphClient, IBccrHttpClient bccrHttpClient)
        {
            _localBitcoinsApiGraphClient = localBitcoinsApiGraphClient;
            _bccrHttpClient = bccrHttpClient;
        }
        
        [FunctionName("UpdateExchangeRateFunction")]
        public async Task Run([TimerTrigger("0 0 0,12,18,19 * * *")]TimerInfo myTimer, ILogger log, CancellationToken cancellationToken = default)
        {
            var date = DateTime.Now;
            var indicators = await _bccrHttpClient.GetExchangeRateAsync(date, default, cancellationToken);
            var indicator = indicators.FirstOrDefault();
            if (indicator == null)
                return;
            
            await _localBitcoinsApiGraphClient.MutationAsync<ExchangeRate>(GraphQlMutation.AddExchangeRate, new {
                FromCurrencyCode = CurrencyCode.CRC,
                ToCurrencyCode = CurrencyCode.USD,
                Date = date,
                Value = indicator.Value
            }, cancellationToken);
        }
    }
}
