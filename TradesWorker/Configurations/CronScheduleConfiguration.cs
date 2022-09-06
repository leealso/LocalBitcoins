using Quartz;

namespace TradesWorker.Configurations;

public class CronScheduleConfiguration
{
    public string UpdateTrades { get; set; }

    public CronScheduleBuilder UpdateTradesCronSchedule => BuildCronSchedule(UpdateTrades);

    private CronScheduleBuilder BuildCronSchedule(string cronSchedule) => CronScheduleBuilder.CronSchedule(cronSchedule);  
}
