using System;
using LocalBitcoins.Functions.Constants;
using LocalBitcoins.Functions.Models;
using Microsoft.EntityFrameworkCore;

namespace LocalBitcoins.Functions.Infrastructure.Data;

public class LocalBitcoinsDbContext : DbContext 
{
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        var connectionString = System.Environment.GetEnvironmentVariable(ApplicationSettings.ConnectionString, EnvironmentVariableTarget.Process);
        options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
    }

    public DbSet<Trade> Trades { get; set; }
}