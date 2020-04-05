using System;
using System.IO;

namespace CheckLinksConsole
{
    public class OutputSettings
    {
        public OutputSettings()
        {
            var date = DateTime.Today.Date.ToString(@"yyyy-MM-dd");
            var time = DateTime.Now.ToString("h-m-tt");
            File = $"checklinks-{date}--{time}.txt";
        }
        public string Folder { get; set; }
        public string File { get; set; }

        public string GetReportFilePath()
        {
            return Path.Combine(Directory.GetCurrentDirectory(), Folder, File);
        }

        public string GetReportDirectory()
        {
            return Path.GetDirectoryName(GetReportFilePath());
        }
    }
}