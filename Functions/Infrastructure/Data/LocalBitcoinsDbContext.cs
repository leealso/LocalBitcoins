using LocalBitcoins.Functions.Constants;
using LocalBitcoins.Functions.Models;
using LocalBitcoins.Functions.Utilities;
using Microsoft.EntityFrameworkCore;

namespace LocalBitcoins.Functions.Infrastructure.Data;

public class LocalBitcoinsDbContext : DbContext 
{
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        var connectionString = ApplicationSettingsUtility.Get(ApplicationSettings.ConnectionString);
        options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
    }

    public DbSet<Trade> Trades { get; set; }
}