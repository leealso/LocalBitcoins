using LocalBitcoinsAPI.Extensions;
using LocalBitcoinsAPI.Infrastructure.Data;
using LocalBitcoinsAPI.Infrastructure.HttpClients;
using LocalBitcoinsAPI.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
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
builder.Services.AddTransient<IDailySummaryService, DailySummaryService>();
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
//app.UseAuthorization();
app.MapGraphQL();

app.Run();
