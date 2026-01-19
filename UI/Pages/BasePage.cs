using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

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

        protected IWebElement WaitForElement(By locator, Func<IWebElement, bool> condition)
        {
            return Wait.Until(driver =>
            {
                var element = driver.FindElement(locator);
                return condition(element) ? element : null;
            });
        }

        protected IWebElement WaitForElementVisible(By locator)
        {
            return WaitForElement(locator, element => element.Displayed);
        }

        protected IWebElement WaitForElementClickable(By locator)
        {
            return WaitForElement(locator, element =>
                element.Displayed && element.Enabled
            );
        }

        // actions
        protected void ClickElement(By locator)
        {
            var element = WaitForElementClickable(locator);

            try
            {
                element.Click(); // try real interaction first
            }
            catch (ElementClickInterceptedException)
            {
                var js = (IJavaScriptExecutor)Driver;
                js.ExecuteScript("arguments[0].click();", element); // fallback to JS click
            }
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