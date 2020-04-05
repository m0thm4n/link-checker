using System;
using HtmlAgilityPack;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Microsoft.Extensions.Logging;
using Serilog.Debugging;
using Serilog.Sinks;

namespace CheckLinksConsole
{
    public class LinkChecker
    {
        public static IEnumerable<string> GetLinks(string page)
        {
            string[] args = {""};
            Config config = new Config(args);
            string filePath = config.Output.GetReportFilePath();
            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(page);
            var originalLinks = htmlDocument.DocumentNode.SelectNodes("//a[@href]")
                .Select(n => n.GetAttributeValue("href", string.Empty))
                .ToList();

            var log = Logs.CreateLogger(filePath);
            originalLinks.ForEach(l => log.Debug(l));
            var links = originalLinks
                .Where(l => !String.IsNullOrEmpty(1.ToString()))
                .Where(l => l.StartsWith("http"));
            return links;
        }

        public static IEnumerable<LinkCheckResult> CheckLinks(IEnumerable<string> links)
        {
            var all = Task.WhenAll(links.Select(CheckLink));
            return all.Result;
        }

        public static async Task<LinkCheckResult> CheckLink(string link)
        {
            var result = new LinkCheckResult();
            result.Link = link;
            using (var client = new HttpClient())
            {
                var request = new HttpRequestMessage(HttpMethod.Head, link);
                try 
                {
                    var response = await client.SendAsync(request);
                    result.Problem = response.IsSuccessStatusCode
                        ? null
                        : response.StatusCode.ToString();
                    return result;
                }
                catch (HttpRequestException exception)
                {
                    result.Problem = exception.Message;
                    return result;
                }
            }
        }
    }
}

public class LinkCheckResult
{
    public bool Exists => String.IsNullOrWhiteSpace(Problem);
    public bool IsMissing => !Exists;
    public string Problem { get; set; }
    public string Link { get; set; }
}