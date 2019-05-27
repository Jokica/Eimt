using Eimt.Application.Jobs;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;

namespace Emit.Web.Scedular
{
    public class Shedular
    {
        public static async Task<IScheduler> ConfigureJobsAsync()
        {
            try
            {
                var properties = new NameValueCollection
                {
                    ["quartz.scheduler.instanceName"] = "QuartzWithCore",
                    ["quartz.threadPool.type"] = "Quartz.Simpl.SimpleThreadPool, Quartz",
                    ["quartz.threadPool.threadCount"] = "3",
                    ["quartz.jobStore.type"] = "Quartz.Simpl.RAMJobStore, Quartz",
                };
                StdSchedulerFactory factory = new StdSchedulerFactory(properties);
                IScheduler scheduler = await factory.GetScheduler();
                await scheduler.Start();
                IJobDetail job = JobBuilder.Create<DeleteOldConfirmationTokensJob>()
                                 .WithIdentity("DeleteTokenJob", "group1")
                                 .Build();
                ITrigger trigger = TriggerBuilder.Create()
                                    .WithIdentity("triger1", "group1")
                                    .StartAt(DateTime.Now.AddSeconds(10))
                                    .WithSimpleSchedule(x =>
                                            x.WithIntervalInSeconds(10)
                                            .RepeatForever())
                                    .Build();
                await scheduler.ScheduleJob(job, trigger);
                return scheduler;

            }
            catch (SchedulerException se)
            {
                await Console.Error.WriteLineAsync(se.ToString());
                throw;
            }
        }
    }
}
