using Eimt.Application.Jobs;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;

namespace Emit.Web.Scedular
{
    public static class Shedular
    {
        public static void AddQuartz(this IServiceCollection services, Type jobType)
        {
            services.Add(new ServiceDescriptor(typeof(IJob), jobType, ServiceLifetime.Transient));
            services.AddSingleton<IJobFactory, ScheduledJobFactory>();
            services.AddSingleton(provider =>
            {
                return JobBuilder.Create<DeleteOldConfirmationTokensJob>()
                  .WithIdentity("Sample.job", "group1")
                  .Build();
            });

            services.AddSingleton(provider =>
            {
                return TriggerBuilder.Create()
                .WithIdentity($"Sample.trigger", "group1")
                .StartNow()
                .WithSimpleSchedule
                 (s =>
                    s.WithInterval(TimeSpan.FromSeconds(5))
                    .RepeatForever()
                 )
                 .Build();
            });

            services.AddSingleton(provider =>
            {
                var schedulerFactory = new StdSchedulerFactory();
                var scheduler = schedulerFactory.GetScheduler().Result;
                scheduler.JobFactory = provider.GetService<IJobFactory>();
                scheduler.Start();
                return scheduler;
            });

        }

        public static void UseQuartz(this IApplicationBuilder app)
        {
            app.ApplicationServices.GetService<IScheduler>()
                .ScheduleJob(app.ApplicationServices.GetService<IJobDetail>(),
                app.ApplicationServices.GetService<ITrigger>()
                );
        }
    }
}
