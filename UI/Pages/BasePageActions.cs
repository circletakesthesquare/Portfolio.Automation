namespace UI.Pages
{
    /// <summary>
    /// Base implementation of IPageActions providing common page interaction methods.
    /// This class will be wrapped by decoroators to add cross-cutting concerns like logging, error handling, or performance monitoring without modifying the core page interaction logic.
    /// </summary>
    public class BasePageActions : IPageActions
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        public BasePageActions(IWebDriver driver, WebDriverWait wait)
        {
            _driver = driver ?? throw new ArgumentNullException(nameof(driver));
            _wait = wait ?? throw new ArgumentNullException(nameof(wait));
        }

        public void GoToUrl(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                throw new ArgumentException("URL cannot be null or empty.", nameof(url));
            
            _driver.Navigate().GoToUrl(url);
        }

        public void ClickElement(By locator)
        {
            var element = WaitForElementClickable(locator);

            try
            {
                element.Click();
            }
            catch (ElementClickInterceptedException)
            {
                // Fallback to JavaScript click if regular click is intercepted
                var js = (IJavaScriptExecutor)_driver;
                js.ExecuteScript("arguments[0].click();", element);
            }
        }

        public void EnterText(By locator, string text)
        {
            if(text == null)
                throw new ArgumentNullException(nameof(text), "Text cannot be null.");

            var element = WaitForElementVisible(locator);
            element.Clear();
            element.SendKeys(text);
        }

        public string GetElementText(By locator)
        {
            return WaitForElementVisible(locator).Text;
        }

        public IWebElement WaitForElementVisible(By locator)
        {
            return WaitForElement(locator, element => element.Displayed);
        }

        public IWebElement WaitForElementClickable(By locator)
        {
            return WaitForElement(locator, element => element.Enabled && element.Displayed);
        }

        public IWebElement WaitForElement(By locator, Func<IWebElement, bool> condition)
        {
            if(locator == null)
                throw new ArgumentNullException(nameof(locator), "Locator cannot be null.");

            if(condition == null)
                throw new ArgumentNullException(nameof(condition), "Condition cannot be null.");

            return _wait.Until(driver =>
            {
                try
                {
                    var element = driver.FindElement(locator);
                    return condition(element) ? element : null;
                }
                catch (NoSuchElementException)
                {
                    return null;
                }
                catch (StaleElementReferenceException)
                {
                    return null;
                }
            });
        }

        public void ScrollToElement(By locator)
        {
            if (locator == null)
                throw new ArgumentNullException(nameof(locator));

            var element = WaitForElementVisible(locator);
            var js = (IJavaScriptExecutor)_driver;

            // Scroll element into view, centered if possible
            js.ExecuteScript("arguments[0].scrollIntoView({behavior: 'smooth', block: 'center', inline: 'center'});", element);
        }

        public bool IsActionSuccessful(By locator, Func<IWebElement, bool> successCondition, int timeoutInSeconds = 5)
        {
            if (locator == null)
                throw new ArgumentNullException(nameof(locator));
            if (successCondition == null)
                throw new ArgumentNullException(nameof(successCondition));
            if (timeoutInSeconds <= 0)
                throw new ArgumentException("Timeout must be greater than 0", nameof(timeoutInSeconds));

            try
            {
                // Create a temporary wait with the specified timeout
                var tempWait = new WebDriverWait(_driver, TimeSpan.FromSeconds(timeoutInSeconds));

                tempWait.Until(driver =>
                {
                    try
                    {
                        var element = driver.FindElement(locator);
                        return successCondition(element);
                    }
                    catch (NoSuchElementException)
                    {
                        return false;
                    }
                    catch (StaleElementReferenceException)
                    {
                        return false;
                    }
                });

                return true;
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        }
    }
}