using System;
using System.IO;
using System.Collections.Generic;
using System.Net.Http;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace CheckLinksConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new Config(args);
            /*var currentDirectory = Directory.GetCurrentDirectory();
            var outputFolder = OutputSettings.Folder;
            var outputFile = OutputSettings.File;*/
            var site = config.Site;
            var output = config.Output;
            var outputPath = output.GetReportFilePath();
            System.Console.WriteLine(outputPath);
            var directory = config.Output.GetReportDirectory();
            if (!String.IsNullOrEmpty(directory))
            {
                Directory.CreateDirectory(output.GetReportDirectory());    
            }
            System.Console.WriteLine($"Saving report to {outputPath}");

            HttpClient client = new HttpClient();
            var body = client.GetStringAsync(site);
            Console.WriteLine(body.Result);

            Console.WriteLine("Links!");
            var links = LinkChecker.GetLinks(body.Result);
            links.ToList().ForEach(Console.WriteLine);
            // write out links
            var checkedLinks = LinkChecker.CheckLinks(links);
            using (var file = File.CreateText(outputPath))
            {
                foreach (var link in checkedLinks.OrderBy(l => l.Exists))
                {
                    var status = link.IsMissing ? "missing" : "OK";
                    file.WriteLine($"{status} - {link.Link}");
                }
            }
        }
    }
}
