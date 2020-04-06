using System;
using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.AspNetCore.Hosting;
using System.IO;

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

            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Startup>()
                .Build();
            
            
            using (var server = new BackgroundJobServer())
            {
                Console.WriteLine("Hangfire Server started.");
                host.Run();
            }  
        }
    }
}