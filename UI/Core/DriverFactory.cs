using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Portfolio.Automation.UI.Core;

namespace UI
{
    public static class DriverFactory
    {
        /// <summary>
        /// Creates and configures a WebDriver instance based on the provided UI configuration.
        /// </summary>
        /// <param name="config">The UI configuration settings.</param>
        /// <returns>An initialized IWebDriver instance.</returns>
        public static IWebDriver CreateWebDriver(UiConfig config)
        {
            IWebDriver driver;

            switch (config.Browser.ToLower())
            {
                case "chrome":
                    var chromeOptions = new ChromeOptions();
                    if (config.Headless)
                    {
                        chromeOptions.AddArgument("--headless");
                        chromeOptions.AddArgument("--window-size=1920,1080");
                    }
                    driver = new ChromeDriver(chromeOptions);
                    break;

                // to do: add other browsers (Firefox, Edge, etc.)

                default:
                    throw new ArgumentException($"Unsupported browser: {config.Browser}");
            }

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(config.DefaultTimeoutSeconds);
            driver.Manage().Window.Maximize();
            return driver;
        }
    }
}