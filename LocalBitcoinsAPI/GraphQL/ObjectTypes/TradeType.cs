using LocalBitcoinsAPI.Models;
using LocalBitcoinsAPI.Services;

namespace LocalBitcoinsAPI.GraphQL.ObjectTypes;

public class TradeType : ObjectType<Trade>
{
    protected override void Configure(IObjectTypeDescriptor<Trade> descriptor)
    {
        descriptor.Field("priceUsd")
            .Type<NonNullType<DecimalType>>()
            .Resolve(async context => 
            {
                var trade = context.Parent<Trade>();
                var exchangeRate = await context.Service<IExchangeRateService>().GetExchangeRateAsync(trade.Date.Date, context.RequestAborted);
                return Math.Round(trade.Price / exchangeRate.Value, 2);
            });
    }
}