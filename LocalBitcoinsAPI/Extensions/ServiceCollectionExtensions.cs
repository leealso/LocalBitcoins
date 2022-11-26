using LocalBitcoinsAPI.GraphQL;
using LocalBitcoinsAPI.GraphQL.DataLoaders;
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
            .AddDiagnosticEventListener<GraphQLDiagnosticEventListener>()
            .AddErrorFilter<GraphQLErrorFilter>()
            .RegisterDbContext<LocalBitcoinsDbContext>()
            .AddQueryType<LocalBitcoinsQuery>()
            .AddMutationType<LocalBitcoinsMutation>()
            .AddDataLoader<ClosedTradesBatchDataLoader>()
            .AddType<TradeType>()
            .AddType<ExchangeRateType>();

        return services;
    }
}