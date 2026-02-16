namespace UI.Pages
{
    /// <summary>
    /// Base class for all page objects, providing common functionality.
    /// Now implements IPageActions to be decorator-friendly, allowing cross-cutting concerns to be added without modifying page logic.
    /// Can be used directly or wrapped by decorators.
    /// </summary>
    public abstract class BasePage : IPageActions
    {
        protected IWebDriver Driver {get;}
        protected WebDriverWait Wait {get;}

        protected BasePage(IWebDriver driver, WebDriverWait wait)
        {
            Driver = driver ?? throw new ArgumentNullException(nameof(driver));
            Wait = wait ?? throw new ArgumentNullException(nameof(wait));
        }

        #region IPageActions Implementation

        public void GoToUrl(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                throw new ArgumentException("URL cannot be null or empty.", nameof(url));

                Driver.Navigate().GoToUrl(url);
        }

        public IWebElement WaitForElement(By locator, Func<IWebElement, bool> condition)
        {
            if(locator == null)
                throw new ArgumentNullException(nameof(locator));

            if(condition == null)
                throw new ArgumentNullException(nameof(condition));

            return Wait.Until(driver =>
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

        public IWebElement WaitForElementVisible(By locator)
        {
            return WaitForElement(locator, element => element.Displayed);
        }

        public IWebElement WaitForElementClickable(By locator)
        {
            return WaitForElement(locator, element =>
                element.Displayed && element.Enabled
            );
        }

        // actions
        public void ClickElement(By locator)
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

        public void ScrollToElement(By locator)
        {
            if (locator == null)
                throw new ArgumentNullException(nameof(locator));

            var element = WaitForElementVisible(locator);
            var js = (IJavaScriptExecutor)Driver;

            // Scroll element into view, centered if possible
            js.ExecuteScript("arguments[0].scrollIntoView({behavior: 'smooth', block: 'center', inline: 'center'});", element);
        }

        public bool IsActionSuccessful(By locator, Func<IWebElement, bool> successCondition, int timeoutInSeconds = 5)
        {
            if (locator == null)
                throw new ArgumentNullException(nameof(locator));

            if (successCondition == null)
                throw new ArgumentNullException(nameof(successCondition));

            try
            {
                var tempWait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeoutInSeconds));

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
                return false; // Action did not lead to expected condition within timeout
            }
        }

        #endregion
    }
}