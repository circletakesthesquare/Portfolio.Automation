using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Portfolio.Automation.UI.Core;

namespace UI
{
    /// <summary>
    /// Base class for UI tests, providing common setup and utilities.
    /// </summary>
    public abstract class TestBase : IDisposable
    {

        protected IWebDriver Driver;
        protected WebDriverWait Wait;
        protected UiConfig Config;

        protected TestBase()
        {
            Config = UiConfig.Load();

            Driver = DriverFactory.CreateWebDriver(Config);

            Wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(Config.DefaultTimeoutSeconds));

            // Navigate to the base URL
            Driver.Navigate().GoToUrl(Config.BaseUrl);       
        }

        public void Dispose()
        {
            try
            {
                Driver?.Quit();
                Driver?.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Teardown Warning] Error during WebDriver disposal: {ex.Message}");

            }
        }

        #region Helper Methods
        protected void RefreshPage() => Driver.Navigate().Refresh();
        protected void NavigateToUrl(string url) => Driver.Navigate().GoToUrl(url);
        #endregion

    }
}