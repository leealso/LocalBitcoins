using LocalBitcoinsAPI.Extensions;
using LocalBitcoinsAPI.Infrastructure.Data;
using LocalBitcoinsAPI.Infrastructure.HttpClients;
using LocalBitcoinsAPI.Services;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

builder.Host.UseSerilog((ctx, lc) => lc
    .WriteTo.Console()
    .ReadFrom.Configuration(ctx.Configuration));

builder.Services.AddCors();
builder.Services.AddPooledDbContextFactory<LocalBitcoinsDbContext>(options => 
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnectionString");
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});
builder.Services.AddMemoryCache();
builder.Services.AddHttpClient<ILocalBitcoinsHttpClient, LocalBitcoinsHttpClient>();
builder.Services.AddHttpClient<ICoinMarketCapHttpClient, CoinMarketCapHttpClient>();
builder.Services.AddTransient<ITradeService, TradeService>();
builder.Services.AddTransient<IClosedTradeService, ClosedTradeService>();
builder.Services.AddTransient<ISummaryService, SummaryService>();
builder.Services.AddTransient<IAdvertisementService, AdvertisementService>();
builder.Services.AddTransient<IQuoteService, QuoteService>();
builder.Services.AddTransient<IExchangeRateService, ExchangeRateService>();
builder.Services.AddCustomGraphQL();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true)
);
app.MapGraphQL();

app.Run();
