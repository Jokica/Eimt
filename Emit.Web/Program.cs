using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Eimt.Application.Jobs;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Quartz;
using Quartz.Impl;

namespace Emit.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
            ConfigureJobsAsync().GetAwaiter().GetResult();
        }

        private static async Task ConfigureJobsAsync()
        {
            try
            {
                NameValueCollection props = new NameValueCollection
                {
                    { "quartz.serializer.type", "binary" }
                };
                StdSchedulerFactory factory = new StdSchedulerFactory(props);
                IScheduler scheduler = await factory.GetScheduler();
                await scheduler.Start();
                IJobDetail job = JobBuilder.Create<DeleteOldConfirmationTokensJob>()
                                 .WithIdentity("DeleteTokenJob", "group1")
                                 .Build();
                ITrigger trigger = TriggerBuilder.Create()
                                    .WithIdentity("triger1", "group1")
                                    .StartNow()
                                    .WithSimpleSchedule(x => 
                                            x.WithIntervalInHours(24)
                                            .RepeatForever())
                                    .Build();
                await scheduler.ScheduleJob(job,trigger);

            }
            catch (SchedulerException se)
            {
                await Console.Error.WriteLineAsync(se.ToString());
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
