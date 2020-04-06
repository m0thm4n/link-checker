using System;
using System.IO;
using System.Net.Http;
using System.Linq;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Sinks;
using Hangfire;
using Hangfire.MemoryStorage;

namespace CheckLinksConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            GlobalConfiguration.Configuration.UseMemoryStorage();

            RecurringJob.AddOrUpdate(() => Console.WriteLine("\n\nRecurring Job\n\n"), Cron.Minutely);

            using (var server = new BackgroundJobServer())
            {
                Console.WriteLine("Hangfire Server started. Press any key to exit...");
                Console.ReadLine();
            }
            return;

         //   var config = new Config(args); 
         //   var log = Logs.CreateLogger(config);
         //   log.Information($"Saving report to {config.Output.GetReportDirectory()}");

         //   HttpClient client = new HttpClient();
         //   var body = client.GetStringAsync(config.Site);
         //   log.Information(body.Result);

         //   var links = LinkChecker.GetLinks(body.Result);
         //   var checkedLinks = LinkChecker.CheckLinks(links);
         //   // using (var file = File.CreateText(config.Output.GetReportDirectory()))
         //   using (var linksDb = new LinksDb())
	        //{
         //       foreach (var link in checkedLinks.OrderBy(l => l.Exists))
         //       {
         //           // var status = link.IsMissing ? "missing" : "OK";
         //           // file.WriteLine($"{status} - {link.Link}");
		       //     linksDb.Links.Add(link);
         //       }
		       // linksDb.SaveChanges();
         //       Console.WriteLine("DB Changes written");
         //   }
        }
    }
}
