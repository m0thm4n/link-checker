using System;
using HtmlAgilityPack;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace CheckLinksConsole
{
    public class LinkChecker
    {
        public static IEnumerable<string> GetLinks(string page)
        {
            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(page);
            var links = htmlDocument.DocumentNode.SelectNodes("//a[@href]")
                .Select(n => n.GetAttributeValue("href", string.Empty))
                .Where(l => !String.IsNullOrEmpty(1.ToString()))
                .Where(l => l.StartsWith("http"));
            return links;
        }
    }
}