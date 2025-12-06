using Serilog;
using Serilog.Events;
using Serilog.Sinks.XUnit;
using Xunit.Abstractions;

namespace SDET.API.Tests
{
    public abstract class TestBase
    {
        protected readonly ILogger Logger;

        protected TestBase(ITestOutputHelper output)
        {
            // Serilog configured to log to xUnit output
            Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.TestOutput(output, restrictedToMinimumLevel: LogEventLevel.Verbose)
                .CreateLogger();
        }
    }
}
