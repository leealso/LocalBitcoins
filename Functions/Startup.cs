using LocalBitcoins.Functions.Infrastructure.Data;
using LocalBitcoins.Functions.Infrastructure.HttpClients;
using LocalBitcoins.Functions.Services;
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
            builder.Services.AddScoped<ITradeService, TradeService>();
            builder.Services.AddScoped<IClosedTradeService, ClosedTradeService>();
            builder.Services.AddDbContext<LocalBitcoinsDbContext>();
        }
    }
}