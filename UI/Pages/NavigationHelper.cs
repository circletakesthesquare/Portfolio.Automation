namespace UI.Pages
{
    /// <summary>
    /// Helper class for navigating the DemoQA site via menus and UI elements.
    /// Use when direct URL navigation is unreliable.
    /// </summary>
    public class NavigationHelper
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;
        private readonly ILogger _logger;

        public NavigationHelper(IWebDriver driver, WebDriverWait wait, ILogger logger)
        {
            _driver = driver;
            _wait = wait;
            _logger = logger;
        }

        /// <summary>
        /// Waits for an element to be visible.
        /// </summary>
        private IWebElement WaitForElementVisible(By locator)
        {
            return _wait.Until(driver =>
            {
                try
                {
                    var element = driver.FindElement(locator);
                    return element.Displayed ? element : null;
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

        /// <summary>
        /// Waits for an element to be clickable and clicks it.
        /// Optionally filters by a condition.
        /// </summary>
        private void ClickElementWhenReady(By locator, Func<IWebElement, bool> condition, string elementDescription)
        {
            _logger.Debug("Waiting for {ElementDescription} to be clickable", elementDescription);

            var element = _wait.Until(driver =>
            {
                try
                {
                    var elements = driver.FindElements(locator);
                    
                    foreach (var el in elements)
                    {
                        if (!el.Displayed || !el.Enabled)
                            continue;

                        if (condition == null || condition(el))
                            return el;
                    }
                    
                    return null;
                }
                catch (StaleElementReferenceException)
                {
                    return null;
                }
            });

            if (element == null)
            {
                throw new NoSuchElementException($"Could not find clickable element: {elementDescription}");
            }

            // Scroll element into view
            ((IJavaScriptExecutor)_driver).ExecuteScript(
                "arguments[0].scrollIntoView({behavior: 'auto', block: 'center'});", 
                element
            );

            // Wait for element to be stable (not moving)
            _wait.Until(driver =>
            {
                try
                {
                    var location1 = element.Location;
                    var location2 = element.Location;
                    return location1.Equals(location2);
                }
                catch
                {
                    return false;
                }
            });

            // Click with fallback to JS click
            try
            {
                element.Click();
                _logger.Debug("Clicked {ElementDescription}", elementDescription);
            }
            catch (ElementClickInterceptedException)
            {
                _logger.Debug("Regular click intercepted, using JavaScript click for {ElementDescription}", elementDescription);
                ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].click();", element);
            }
        }

        /// <summary>
        /// Generic method to navigate via category and menu item.
        /// </summary>
        public void NavigateToPage(string category, string menuItem)
        {
            _logger.Information("Navigating to {Category} > {MenuItem}", category, menuItem);

            // Click category card
            ClickElementWhenReady(
                By.CssSelector(".card-body h5"),
                element => element.Text.Equals(category, StringComparison.OrdinalIgnoreCase),
                $"{category} category"
            );

            // Wait for sidebar
            WaitForElementVisible(By.CssSelector(".left-pannel"));

            // Click menu item (try multiple strategies)
            try
            {
                // Strategy 1: Try by ID pattern (most reliable if available)
                var menuItemNormalized = menuItem.Replace(" ", "").ToLower();
                try
                {
                    ClickElementWhenReady(
                        By.Id($"item-{menuItemNormalized}"),
                        null,
                        menuItem
                    );
                    return;
                }
                catch { }

                // Strategy 2: Try by text in menu list
                ClickElementWhenReady(
                    By.CssSelector(".menu-list li"),
                    element => element.Text.Contains(menuItem, StringComparison.OrdinalIgnoreCase),
                    menuItem
                );
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to navigate to {Category} > {MenuItem}", category, menuItem);
                throw;
            }

            _logger.Information("Successfully navigated to {MenuItem}", menuItem);
        }


        // Convenience methods for common pages

        /// <summary>
        /// Navigates to the Text Box page via Elements menu.
        /// </summary>
        public void NavigateToTextBox()
        {
            _logger.Information("Navigating to Text Box via menu");

            // Click "Elements" category card
            ClickElementWhenReady(
                By.CssSelector(".card-body h5"),
                element => element.Text == "Elements",
                "Elements category card"
            );

            // Wait for sidebar menu to be visible
            WaitForElementVisible(By.CssSelector(".left-pannel"));

            // Click "Text Box" menu item in sidebar
            ClickElementWhenReady(
                By.CssSelector(".menu-list li span.text"),    // Locator: find spans with class "text"
                element => element.Text.Trim() == "Text Box",  // Condition: filter for one with text "Text Box"
                "Text Box menu item"                           // Description: for logging only
            );

            // Wait for page to load by checking for form presence
            WaitForElementVisible(By.Id("userName"));
            
            _logger.Information("Successfully navigated to Text Box page");
        }
    }
}