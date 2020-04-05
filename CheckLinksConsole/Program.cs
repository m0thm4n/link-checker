using System;
using System.IO;
using System.Collections.Generic;
using System.Net.Http;
using System.Linq;

namespace CheckLinksConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var outputFolder = "reports";
            var outputFile = "report.txt";
            var outputPath = Path.Combine(currentDirectory, outputFolder, outputFile);
            System.Console.WriteLine(outputPath);
            var directory = Path.GetDirectoryName(outputPath);
            System.Console.WriteLine(directory);
            Directory.CreateDirectory(directory);
            System.Console.WriteLine($"Saving report to {outputPath}");
            // string site = "https://g0t4.github.io/pluralsight-dotnet-core-xplat-apps";
            // string site = args[0];
            HttpClient client = new HttpClient();
            var body = client.GetStringAsync(site);
            Console.WriteLine(body.Result);

            Console.WriteLine("Links!");
            var links = LinkChecker.GetLinks(body.Result);
            links.ToList().ForEach(Console.WriteLine);
            // write out links
            // File.WriteAllLinesAsync(outputPath, links);

            var checkedLinks = LinkChecker.CheckLinks(links);
            using (var file = File.CreateText(outputPath))
            {
                foreach (var link in checkedLinks.OrderBy(l => l.Exists))
                {
                    var status = link.IsMissing ? "missing" : "OK";
                    file.WriteLineAsync($"{status} - {link.Link}");
                }
            }
        }
    }
}
