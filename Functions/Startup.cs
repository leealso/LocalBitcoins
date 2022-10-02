using System;
using GraphQL.Client.Abstractions.Websocket;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using LocalBitcoins.Functions.Constants;
using LocalBitcoins.Functions.Infrastructure.Data;
using LocalBitcoins.Functions.Infrastructure.HttpClients;
using LocalBitcoins.Functions.Services;
using LocalBitcoins.Functions.Utilities;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(LocalBitcoins.Functions.Startup))]

namespace LocalBitcoins.Functions
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddHttpClient<ILocalBitcoinsHttpClient, LocalBitcoinsHttpClient>();
            builder.Services.AddHttpClient<IBccrHttpClient, BccrHttpClient>();
            builder.Services.AddSingleton<IGraphQLWebsocketJsonSerializer, NewtonsoftJsonSerializer>();
            var localBitcoinsApiUrl = new Uri(ApplicationSettingsUtility.Get(ApplicationSettings.BccrIndicatorsToken));
            builder.Services.AddHttpClient<GraphQLHttpClient>();
            builder.Services.AddSingleton(new GraphQLHttpClientOptions { EndPoint = localBitcoinsApiUrl });
            builder.Services.AddScoped<ILocalBitcoinsApiGraphClient, LocalBitcoinsApiGraphClient>();
            builder.Services.AddScoped<ITradeService, TradeService>();
            builder.Services.AddScoped<IClosedTradeService, ClosedTradeService>();
            builder.Services.AddDbContext<LocalBitcoinsDbContext>();
        }
    }
}