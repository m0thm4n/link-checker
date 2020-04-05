using Serilog;
using Serilog.Core;

namespace CheckLinksConsole
{
    public class Logs
    {
        public static Logger CreateLogger(string filePath)
        {
            var log = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.Trace()
                .WriteTo.Debug()
                .WriteTo.File(filePath)
                .CreateLogger();

            return log;
        }

        public static Logger CreateLogger(Config config)
        {
            var log = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.Trace()
                .WriteTo.Debug()
                .WriteTo.File(config.Output.GetReportFilePath())
                .CreateLogger();

            return log;
        }
    }
}