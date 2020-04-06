using System;
using System.Net.Http;
using System.Linq;
using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.Extensions.Logging;
using Serilog.Core;

namespace CheckLinksConsole
{
    class Program
    {
        static void Main(string[] args)
        {
           var config = new Config(args); 
           var log = Logs.CreateLogger(config);


            GlobalConfiguration.Configuration.UseMemoryStorage();

            RecurringJob.AddOrUpdate<CheckLinkJob>("check-link", j => j.Execute(config.Site, config.Output), Cron.Minutely);
            RecurringJob.Trigger("check-link");

            using (var server = new BackgroundJobServer())
            {
                Console.WriteLine("Hangfire Server started. Press any key to exit...");
                Console.ReadLine();
            }  
        }
    }
}