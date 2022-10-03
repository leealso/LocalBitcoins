using LocalBitcoinsAPI.Models;
using LocalBitcoinsAPI.Services;

namespace LocalBitcoinsAPI.GraphQL.ObjectTypes;

public class ExchangeRateType : ObjectType<ExchangeRate>
{
    protected override void Configure(IObjectTypeDescriptor<ExchangeRate> descriptor)
    {
        descriptor.Field("percentChange24h")
            .Type<NonNullType<DecimalType>>()
            .Resolve(async context => 
            {
                var exchangeRate = context.Parent<ExchangeRate>();
                var yesterday = exchangeRate.Date.AddDays(-1).Date;
                var yesterdayExchangeRate = await context.Service<IExchangeRateService>().GetExchangeRateAsync(yesterday, context.RequestAborted);
                return Math.Round(exchangeRate.Value / yesterdayExchangeRate.Value - 1, 2);
            });
    }
}