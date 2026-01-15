

namespace UI.Pages
{
    /// <summary>
    /// Base class for all page objects, providing common functionality.
    /// </summary>
    public abstract class BasePage
    {
        protected IWebDriver Driver {get;}
        protected WebDriverWait Wait {get;}


        protected BasePage(IWebDriver driver, WebDriverWait wait)
        {
            Driver = driver;
            Wait = wait;
        }

        #region Generic Methods

        public void GoToUrl(string url)
        {
            Driver.Navigate().GoToUrl(url);
        }

        protected IWebElement WaitForElementVisible(By locator, int timeoutSeconds = 10)
        {
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeoutSeconds));

            return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(locator));
        }

        protected IWebElement WaitForElementClickable(By locator, int timeoutSeconds = 10)
        {
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeoutSeconds));

            return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(locator));
        }

        // actions
        protected void ClickElement(By locator)
        {
            var element = WaitForElementClickable(locator);
            element.Click();
        }

        protected void EnterText(By locator, string text)
        {
            var element = WaitForElementVisible(locator);
            element.Clear();
            element.SendKeys(text);
        }

        protected string GetElementText(By locator)
        {
            return WaitForElementVisible(locator).Text;
        }

        #endregion
    }
}