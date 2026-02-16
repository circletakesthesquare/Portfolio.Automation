namespace UI.Pages
{
    /// <summary>
    /// Interface degining all common page actions.
    /// Enables true decorator pattern implementation for page objects, allowing for flexible and reusable page interactions.
    /// Any classs implementing this interface can be wrapped by decorators.
    /// </summary>
    public interface IPageActions
    {
        void GoToUrl(string url);
        void ClickElement(By locator);
        void EnterText(By locator, string text);
        string GetElementText(By locator);
        IWebElement WaitForElementVisible(By locator);
        IWebElement WaitForElementClickable(By locator);
        /// <summary>
        /// Waits for an element to meet a custom condition and returns it.
        /// </summary>
        IWebElement WaitForElement(By locator, Func<IWebElement, bool> condition);
        void ScrollToElement(By locator);
        /// <summary>
        /// Performs an action and waits for a specific condition to be met on a target element, returning true if successful within the timeout.
        /// Common use: verify element state after an action (e.g., button became disabled after click).
        /// </summary>
        bool IsActionSuccessful(By locator, Func<IWebElement, bool> successCondition, int timeoutInSeconds = 5);
    }
}