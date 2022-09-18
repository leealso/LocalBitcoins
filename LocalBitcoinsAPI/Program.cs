using LocalBitcoinsAPI.GraphQL;
using LocalBitcoinsAPI.Infrastructure.Data;
using LocalBitcoinsAPI.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("*");
    });
});
builder.Services.AddPooledDbContextFactory<LocalBitcoinsDbContext>(options => 
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnectionString");
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});
builder.Services.AddTransient<ITradeService, TradeService>();
builder.Services.AddTransient<IClosedTradeService, ClosedTradeService>();
builder.Services
    .AddGraphQLServer()
    .AddFiltering()
    .AddSorting()
    .RegisterDbContext<LocalBitcoinsDbContext>()
    .AddQueryType<LocalBitcoinsQuery>();
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
/*if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}*/

app.UseHttpsRedirection();

app.UseCors();

//app.UseAuthorization();

app.MapGraphQL();

app.Run();
