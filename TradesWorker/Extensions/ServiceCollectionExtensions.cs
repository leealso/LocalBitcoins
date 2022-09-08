using Quartz;
using TradesWorker.Configurations;
using TradesWorker.Infrastructure.CronJobs;

namespace TradesWorker.Extensions;

public static class ServiceCollectionExtensions 
{
    public static IServiceCollection ConfigureSection<TConfiguration>(this IServiceCollection services, IConfiguration configuration, string sectionName = null)
        where TConfiguration : class
    {
        if (string.IsNullOrEmpty(sectionName))
            sectionName = GetDefaultSectionName<TConfiguration>();
        services.Configure<TConfiguration>(options => configuration.GetSection(sectionName).Bind(options));
        return services;
    }

    public static IServiceCollection AddCronJobs(this IServiceCollection services, IConfiguration configuration)
    {
        var cronSchedule = configuration.GetSection("CronSchedule").Get<CronScheduleConfiguration>();
        services.AddQuartz(x => 
        {
            x.SchedulerId = "LocalBitcoins";
            x.UseMicrosoftDependencyInjectionJobFactory();

            x.ScheduleJob<UpdateTradesJob>(t => t
                .WithIdentity(nameof(UpdateTradesJob))
                .WithSchedule(cronSchedule.UpdateTradesCronSchedule)
            );
        });

        services.AddQuartzHostedService(options => 
        {
            options.WaitForJobsToComplete = true;
        });

        return services;
    }

    private static string GetDefaultSectionName<TConfiguration>() => 
        typeof(TConfiguration).Name.Replace("Configuration", string.Empty, StringComparison.InvariantCultureIgnoreCase);
}