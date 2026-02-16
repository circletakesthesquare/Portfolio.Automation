namespace UI.Logging
{
    /// <summary>
    /// Centralized Serilog configuration for UI tests.
    /// </summary>
    public static class LoggerConfig
    {
        /// <summary>
        /// Creates a configured Serilog logger for a test.
        /// </summary>
        public static ILogger CreateLogger(string testName = null)
        {
            return new LoggerConfiguration()
                .MinimumLevel.Debug() // Capture all levels of logs
                .Enrich.WithProperty("TestName", testName ?? "UnnamedTest") // Add test name to logs
                .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] ({TestName}) {Message:lj}{NewLine}{Exception}")
                .CreateLogger();
        }
    }
}