using Quartz;
using ToDo.ContentNotifier.Jobs;

namespace ToDo.ContentNotifier;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCustomQuartz(this IServiceCollection services)
    {
        services.AddQuartz(quartzConfigurator =>
        {
            quartzConfigurator.SchedulerId = "ToDo.ContentNotifier";
            quartzConfigurator.SchedulerName = "ToDo Management content notifier";

            quartzConfigurator.UsePersistentStore(s =>
            {
                s.PerformSchemaValidation = true; // default
                s.UseProperties = true; // preferred, but not default
                s.RetryInterval = TimeSpan.FromSeconds(15);
                s.UseSqlServer(sqlServer =>
                {
                    sqlServer.ConnectionString = "Server=localhost;Database=master;User Id=sa;Password=n@s@;TrustServerCertificate=true";
                    sqlServer.TablePrefix = "ToDoManagementQuartz";
                });
                s.UseClustering(c =>
                {
                    c.CheckinMisfireThreshold = TimeSpan.FromSeconds(20);
                    c.CheckinInterval = TimeSpan.FromSeconds(10);
                });
            });

            quartzConfigurator.UseSimpleTypeLoader();
            quartzConfigurator.UseInMemoryStore();
            quartzConfigurator.UseDefaultThreadPool(tp =>
            {
                tp.MaxConcurrency = 10;
            });

            quartzConfigurator.AddJob<NotifierJob>(jobConfigurator =>
            {
                jobConfigurator
                    .WithDescription(NotifierJob.Description)
                    .WithIdentity(NotifierJob.Key);
            });

            quartzConfigurator.AddTrigger(triggerConfigurator => triggerConfigurator
                .WithIdentity("Start up trigger for notifier job")
                .ForJob(NotifierJob.Key)
                .StartNow());

            quartzConfigurator.AddTrigger(triggerConfigurator => triggerConfigurator
                .WithIdentity("Daily trigger for notifier job")
                .ForJob(NotifierJob.Key)
                .WithSchedule(CronScheduleBuilder.DailyAtHourAndMinute(16, 1)));

        });

        services.AddQuartzHostedService();

        return services;
    }
}