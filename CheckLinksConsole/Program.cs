using System;
using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Hangfire.SqlServer;

namespace CheckLinksConsole
{
    class Program
    {
        static void Main(string[] args)
        {

           var config = new Config(args); 
           var log = Logs.CreateLogger(config);


            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Startup>()
                .Build();
                
            JobStorage.Current = new SqlServerStorage(@"Server=192.168.1.87;Database=Links;User Id=sa; Password=whatever12!");


            RecurringJob.AddOrUpdate(() => System.Console.WriteLine("Simple!"), Cron.Minutely);  
            // RecurringJob.AddOrUpdate<CheckLinkJob>("check-link", j => j.Execute(config.Site, config.Output), Cron.Minutely);
            // RecurringJob.Trigger("check-link");
            using (var server = new BackgroundJobServer())
            {
                Console.WriteLine("Hangfire Server started. Press any key to exit...");
                host.Run();
            }
        }
    }
}