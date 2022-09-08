using Microsoft.EntityFrameworkCore;
using TradesWorker.Models;

namespace TradesWorker.Infrastructure.Data;

public class LocalBitcoinsDbContext : DbContext 
{
    protected readonly IConfiguration Configuration;

    public LocalBitcoinsDbContext(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        var connectionString = Configuration.GetConnectionString("DefaultConnection");
        options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
    }

    public DbSet<Trade> Trades { get; set; }
}