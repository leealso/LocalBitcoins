using LocalBitcoinsAPI.GraphQL;
using LocalBitcoinsAPI.GraphQL.ObjectTypes;
using LocalBitcoinsAPI.Infrastructure.Data;

namespace LocalBitcoinsAPI.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCustomGraphQL(this IServiceCollection services)
    {
        services
            .AddGraphQLServer()
            .AddFiltering()
            .AddSorting()
            .RegisterDbContext<LocalBitcoinsDbContext>()
            .AddQueryType<LocalBitcoinsQuery>()
            .AddMutationType<LocalBitcoinsMutation>()
            .AddType<TradeType>()
            .AddType<ExchangeRateType>();

        return services;
    }
}