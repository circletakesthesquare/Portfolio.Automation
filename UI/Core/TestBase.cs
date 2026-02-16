namespace UI
{
    /// <summary>
    /// Base class for UI tests, providing common setup and utilities.
    /// </summary>
    public abstract class TestBase
    {

        protected IWebDriver Driver = null!;
        protected WebDriverWait Wait = null!;
        protected UiConfig Config = null!;
        protected ILogger Logger = null!;

        [SetUp]
        public void Setup()
        {
            // Get test name from NUnit context for logging
            var testName = TestContext.CurrentContext.Test.Name;

            // Initialize logger
            Logger = LoggerConfig.CreateLogger(testName);

            Logger.Information("========================================");
            Logger.Information("Starting Test: {TestName}", testName);
            Logger.Information("========================================");

            // Load configuration

            Config = UiConfig.Load();
            Logger.Debug("Configuration loaded: {@Config}", new { Config.BaseUrl, Config.Browser, Config.DefaultTimeoutSeconds });

            // Initialize WebDriver
            Driver = DriverFactory.CreateWebDriver(Config);
            Logger.Information("WebDriver initialized: {Browser}", Config.Browser);

            // Initialize Wait
            Wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(Config.DefaultTimeoutSeconds));
            Logger.Debug("WebDriverWait configured with {Timeout} second timeout", Config.DefaultTimeoutSeconds);

            // Navigate to the base URL
            Driver.Navigate().GoToUrl(Config.BaseUrl);
            Logger.Information("Navigated to base URL: {BaseUrl}", Config.BaseUrl);
            
        }

        [TearDown]
        public void TearDown()
        {
            var testName = TestContext.CurrentContext.Test.Name;
            var outcome = TestContext.CurrentContext.Result.Outcome.Status;
            var message = TestContext.CurrentContext.Result.Message;
            
            Logger.Information("========================================");
            Logger.Information("Test Finished: {TestName}", testName);
            Logger.Information("Status: {Status}", outcome);
            
            if (!string.IsNullOrEmpty(message))
            {
                var truncatedMessage = message.Length > 53 ? message.Substring(0, 50) + "..." : message;
                Logger.Information("║  Message: {Message,-53}║", truncatedMessage);
            }
            
            Logger.Information("========================================");

            try
            {
                Driver?.Quit();
                Driver?.Dispose();
                Logger.Debug("WebDriver disposed successfully");
            }
            catch (Exception ex)
            {
                Logger.Warning(ex, "Error during WebDriver disposal");
            }
            finally
            {
                // Ensure all logs are flushed
                Log.CloseAndFlush();
            }
        }

        #region Helper Methods
        protected void RefreshPage()
        {
            Logger.Debug("Refreshing page");
            Driver.Navigate().Refresh();
        }
        protected void NavigateToUrl(string url) 
        { 
            Logger.Debug("Navigating to URL: {Url}", url);
            Driver.Navigate().GoToUrl(url);
        }
        #endregion

    }
}