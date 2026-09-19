using Infrastructure.BackgroundJobs;
using Quartz;

namespace Laboratory_Management_API.Extensions
{
    public static class QuartzExtension
    {
        public static IServiceCollection AddBackgroundJobs(this IServiceCollection services)
        {
            services.AddQuartz(config =>
            {
                var jobKey = new JobKey(nameof(ProcessOutboxMessageJob));

                config.AddJob<ProcessOutboxMessageJob>(opts => opts.WithIdentity(jobKey))
                    .AddTrigger(trigger => trigger
                        .ForJob(jobKey)
                        .WithSimpleSchedule(schedule => schedule
                            .WithInterval(TimeSpan.FromSeconds(10))
                            .RepeatForever()));
            });

            services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

            return services;
        }
    }
}