namespace UI.Pages
{
    /// <summary>
    /// Base page that internally uses decorated actions for logging.
    /// Pages inherit from this to get automatic logging without manual decorator setup.
    /// </summary>
    public abstract class DecoratedBasePage : IPageActions
    {
        private readonly IPageActions _decoratedActions;
        
        protected IWebDriver Driver { get; }
        protected WebDriverWait Wait { get; }
        protected ILogger Logger { get; }

        protected DecoratedBasePage(IWebDriver driver, WebDriverWait wait, ILogger logger)
        {
            Driver = driver ?? throw new ArgumentNullException(nameof(driver));
            Wait = wait ?? throw new ArgumentNullException(nameof(wait));
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));

            // Build the decorator chain:
            // Start with concrete implementation
            var baseActions = new BasePageActions(driver, wait);
            
            // Wrap with logging decorator
            _decoratedActions = new LoggingPageActionsDecorator(
                baseActions, 
                logger, 
                contextName: GetType().Name // Use actual page class name for context
            );
        }

        #region IPageActions - Delegate to Decorated Actions

        public void GoToUrl(string url) 
            => _decoratedActions.GoToUrl(url);

        public void ClickElement(By locator) 
            => _decoratedActions.ClickElement(locator);

        public void EnterText(By locator, string text) 
            => _decoratedActions.EnterText(locator, text);

        public string GetElementText(By locator) 
            => _decoratedActions.GetElementText(locator);

        public IWebElement WaitForElementVisible(By locator) 
            => _decoratedActions.WaitForElementVisible(locator);

        public IWebElement WaitForElementClickable(By locator) 
            => _decoratedActions.WaitForElementClickable(locator);

        public IWebElement WaitForElement(By locator, Func<IWebElement, bool> condition) 
            => _decoratedActions.WaitForElement(locator, condition);

        public void ScrollToElement(By locator) 
            => _decoratedActions.ScrollToElement(locator);

        public bool IsActionSuccessful(By locator, Func<IWebElement, bool> condition, int timeoutSeconds = 5) 
            => _decoratedActions.IsActionSuccessful(locator, condition, timeoutSeconds);

        #endregion
    }
}