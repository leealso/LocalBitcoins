using TradesWorker.Configurations;
using TradesWorker.Extensions;
using TradesWorker.Infrastructure.HttpClients;
using TradesWorker.Infrastructure.Data;
using TradesWorker.Services;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.ConfigureSection<LocalBitcoinsConfiguration>(context.Configuration);
        services.AddHttpClient<ILocalBitcoinsHttpClient, LocalBitcoinsHttpClient>();
        services.AddScoped<ITradeService, TradeService>();
        services.AddDbContext<LocalBitcoinsDbContext>();
        services.AddCronJobs(context.Configuration);
    })
    .Build();

await host.RunAsync();
