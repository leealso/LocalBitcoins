using LocalBitcoinsAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LocalBitcoinsAPI.Infrastructure.Data;

public class LocalBitcoinsDbContext : DbContext 
{
    public LocalBitcoinsDbContext(DbContextOptions<LocalBitcoinsDbContext> options)
        : base(options)
    {
        
    }

    public DbSet<Trade> Trades { get; set; }

    public DbSet<ClosedTrade> ClosedTrades { get; set; }

    public DbSet<Currency> Currencies { get; set; }

    public DbSet<ExchangeRate> ExchanteRates { get; set; }
}