namespace UI.Decorators
{
    /// <summary>
    /// A decorator that adds logging capabilities to IPageActions implementations.
    /// This is a pure decorator, it wraps another IPageActions instance via composition.
    /// 
    /// Features:
    /// - Logs method entry with parameters
    /// - Logs execution time
    /// - Logs success/failure
    /// - Masks sensitive data (emails, passwords)
    /// - Provides context for debugging
    /// </summary>   
    public class LoggingPageActionsDecorator : IPageActions
    {
        private readonly IPageActions _innerPageActions;
        private readonly ILogger _logger;
        private readonly string _contextName;

        /// <summary>
        /// Creates a new logging decorator.
        /// </summary>
        /// <param name="innerActions">The actual page actions to wrap</param>
        /// <param name="logger">Serilog ILogger for output</param>
        /// <param name="contextName">Context name for log messages (usually page class name)</param>
        public LoggingPageActionsDecorator(IPageActions innerPageActions, ILogger logger, string contextName = "PageActions")
        {
            _innerPageActions = innerPageActions ?? throw new ArgumentNullException(nameof(innerPageActions));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _contextName = contextName ?? "PageActions";
        }

        public void GoToUrl(string url)
        {
            ExecuteWithLogging(
                methodName: nameof(GoToUrl),
                action: () => _innerPageActions.GoToUrl(url),
                parameters: new { url }
            );
        }

        public void ClickElement(By locator)
        {
            ExecuteWithLogging(
                methodName: nameof(ClickElement),
                action: () => _innerPageActions.ClickElement(locator),
                parameters: new { locator = locator.ToString() }
            );
        }

        public void EnterText(By locator, string text)
        {
            var maskedText = MaskSensitiveData(locator, text);
            ExecuteWithLogging(
                methodName: nameof(EnterText),
                action: () => _innerPageActions.EnterText(locator, text),
                parameters: new { locator = locator.ToString(), text = maskedText }
            );
        }

        public string GetElementText(By locator)
        {
            return ExecuteWithLogging(
                methodName: nameof(GetElementText),
                func: () => _innerPageActions.GetElementText(locator),
                parameters: new { locator = locator.ToString() }
            );
        }

        public IWebElement WaitForElementVisible(By locator)
        {
            return ExecuteWithLogging(
                methodName: nameof(WaitForElementVisible),
                func: () => _innerPageActions.WaitForElementVisible(locator),
                parameters: new { locator = locator.ToString() }
            );
        }

        public IWebElement WaitForElementClickable(By locator)
        {
            return ExecuteWithLogging(
                methodName: nameof(WaitForElementClickable),
                func: () => _innerPageActions.WaitForElementClickable(locator),
                parameters: new { locator = locator.ToString() }
            );
        }

        public IWebElement WaitForElement(By locator, Func<IWebElement, bool> condition)
        {
            return ExecuteWithLogging(
                methodName: nameof(WaitForElement),
                func: () => _innerPageActions.WaitForElement(locator, condition),
                parameters: new { locator = locator.ToString(), hasCondition = condition != null }
            );
        }

        public void ScrollToElement(By locator)
        {
            ExecuteWithLogging(
                methodName: nameof(ScrollToElement),
                action: () => _innerPageActions.ScrollToElement(locator),
                parameters: new { locator = locator.ToString() }
            );
        }

        public bool IsActionSuccessful(By locator, Func<IWebElement, bool> condition, int timeoutSeconds = 5)
        {
            return ExecuteWithLogging(
                methodName: nameof(IsActionSuccessful),
                func: () => _innerPageActions.IsActionSuccessful(locator, condition, timeoutSeconds),
                parameters: new { locator = locator.ToString(), timeoutSeconds, hasCondition = condition != null }
            );
        }

        #region Logging Helper Methods

        /// <summary>
        /// Executes an action with comprehensive logging (for void methods).
        /// </summary>
        private void ExecuteWithLogging(string methodName, Action action, object parameters = null)
        {
            var stopwatch = Stopwatch.StartNew();
            LogEntry(methodName, parameters);

            try
            {
                action();
                stopwatch.Stop();
                LogSuccess(methodName, stopwatch.Elapsed);
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                LogFailure(methodName, ex, stopwatch.Elapsed, parameters);
                throw; // Re-throw to preserve stack trace
            }
        }

        /// <summary>
        /// Executes a function with comprehensive logging (for methods that return values).
        /// </summary>
        private T ExecuteWithLogging<T>(string methodName, Func<T> func, object parameters = null)
        {
            var stopwatch = Stopwatch.StartNew();
            LogEntry(methodName, parameters);

            try
            {
                var result = func();
                stopwatch.Stop();
                LogSuccess(methodName, stopwatch.Elapsed, result);
                return result;
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                LogFailure(methodName, ex, stopwatch.Elapsed, parameters);
                throw; // Re-throw to preserve stack trace
            }
        }

        private void LogEntry(string methodName, object parameters)
        {
            if (parameters != null)
            {
                _logger.Information(
                    "[{Context}] → {MethodName} | Parameters: {@Parameters}", 
                    _contextName, 
                    methodName, 
                    parameters
                );
            }
            else
            {
                _logger.Information(
                    "[{Context}] → {MethodName}", 
                    _contextName, 
                    methodName
                );
            }
        }

        private void LogSuccess(string methodName, TimeSpan elapsed, object result = null)
        {
            var elapsedMs = Math.Round(elapsed.TotalMilliseconds, 2);
            
            if (result != null)
            {
                if (result is string stringResult)
                {
                    _logger.Information(
                        "[{Context}] ✓ {MethodName} completed in {ElapsedMs}ms | Result length: {ResultLength} chars", 
                        _contextName, 
                        methodName, 
                        elapsedMs, 
                        stringResult.Length
                    );
                }
                else if (result is bool boolResult)
                {
                    _logger.Information(
                        "[{Context}] ✓ {MethodName} completed in {ElapsedMs}ms | Result: {Result}", 
                        _contextName, 
                        methodName, 
                        elapsedMs, 
                        boolResult
                    );
                }
                else if (result is IWebElement)
                {
                    _logger.Information(
                        "[{Context}] ✓ {MethodName} completed in {ElapsedMs}ms | Element found", 
                        _contextName, 
                        methodName, 
                        elapsedMs
                    );
                }
                else
                {
                    _logger.Information(
                        "[{Context}] ✓ {MethodName} completed in {ElapsedMs}ms | Result: {ResultType}", 
                        _contextName, 
                        methodName, 
                        elapsedMs, 
                        result.GetType().Name
                    );
                }
            }
            else
            {
                _logger.Information(
                    "[{Context}] ✓ {MethodName} completed in {ElapsedMs}ms", 
                    _contextName, 
                    methodName, 
                    elapsedMs
                );
            }
        }

        private void LogFailure(string methodName, Exception exception, TimeSpan elapsed, object parameters = null)
        {
            var elapsedMs = Math.Round(elapsed.TotalMilliseconds, 2);
            
            _logger.Error(
                exception,
                "[{Context}] ✗ {MethodName} FAILED after {ElapsedMs}ms | Exception: {ExceptionType} | {@Parameters}",
                _contextName,
                methodName,
                elapsedMs,
                exception.GetType().Name,
                parameters
            );
        }

        /// <summary>
        /// Masks sensitive data in text fields based on locator or content.
        /// </summary>
        private string MaskSensitiveData(By locator, string text)
        {
            if (string.IsNullOrEmpty(text)) 
                return text;

            var locatorString = locator.ToString().ToLower();
            var textLower = text.ToLower();

            // Mask if locator contains sensitive field names
            if (locatorString.Contains("password") || 
                locatorString.Contains("pwd") ||
                locatorString.Contains("secret") ||
                locatorString.Contains("token"))
            {
                return "***MASKED_PASSWORD***";
            }

            // Mask if text looks like an email
            if (text.Contains("@") && text.Contains("."))
            {
                return $"***MASKED_EMAIL*** (length: {text.Length})";
            }

            // Mask if text contains password-like patterns
            if (textLower.Contains("password"))
            {
                return "***MASKED_SENSITIVE***";
            }

            return text;
        }

        #endregion


    }
}