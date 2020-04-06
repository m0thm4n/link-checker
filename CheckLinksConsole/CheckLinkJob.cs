using System;
using System.Linq;
using System.Net.Http;
using Serilog.Core;

namespace CheckLinksConsole
{
     public class CheckLinkJob
    {
        public CheckLinkJob()
        {
        }

        public void Execute(string site, OutputSettings output)
            {
                string[] args = {""};
                Config config = new Config(args);
                string filePath = config.Output.GetReportFilePath();
                var log = Logs.CreateLogger(filePath);
                log.Information($"Saving report to {output.GetReportDirectory()}");

                HttpClient client = new HttpClient();
                var body = client.GetStringAsync(site);
                log.Information(body.Result);

                var links = LinkChecker.GetLinks(body.Result);
                var checkedLinks = LinkChecker.CheckLinks(links);
                // using (var file = File.CreateText(output.GetReportDirectory()))
                using (var linksDb = new LinksDb())
                {
                    foreach (var link in checkedLinks.OrderBy(l => l.Exists))
                    {
                        // var status = link.IsMissing ? "missing" : "OK";
                        // file.WriteLine($"{status} - {link.Link}");
                        linksDb.Links.Add(link);
                    }
                    linksDb.SaveChanges();
                    Console.WriteLine("DB Changes written");
                }
        }
    }
}