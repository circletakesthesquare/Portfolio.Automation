using SDET.API.Tests.Utilities;
using Serilog;
using Xunit.Abstractions;

namespace SDET.API.Tests
{
    /// <summary>
    /// Base class for API tests, providing common setup and utilities.
    /// </summary>
    public abstract class TestBase
    {
        protected TestBase(ITestOutputHelper output)
        {

            // Configure Serilog
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.Console()
                .WriteTo.File($"logs/test-log-{DateTime.Now:yyyyMMdd_HHmmss}.txt", rollingInterval: RollingInterval.Day)
                .WriteTo.TestOutput(output)
                .CreateLogger();
        }

        /// <summary>
        /// Creates and configures an HttpClient with logging capabilities.
        /// </summary>
        /// <returns></returns>
        protected HttpClient CreateHttpClient()
        {
            var handler = new LoggingHttpHandler();
            return new HttpClient(handler)
            {
                BaseAddress = new Uri(Config.BaseUrl),
                Timeout = TimeSpan.FromSeconds(Convert.ToInt32(Config.TimeoutSeconds))
            };
        }
    }
}