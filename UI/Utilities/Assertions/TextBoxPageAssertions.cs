namespace UI.Utilities.Assertions
{
    /// <summary>
    /// Assertion methods for TextBoxPage.
    /// </summary>
    public class TextBoxPageAssertions
    {
        private readonly TextBoxPage _page;
        private readonly ILogger _logger;

        public TextBoxPageAssertions(TextBoxPage page, ILogger logger)
        {
            _page = page ?? throw new ArgumentNullException(nameof(page));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Asserts that the output section is displayed and populated after form submission.
        /// </summary>
        public void AssertOutputIsDisplayed()
        {
            _logger.Information("Asserting output is displayed");
            
            Assert.That(_page.IsOutputPopulated(), Is.True, 
                "Output section should be displayed with data after form submission");
            
            _logger.Information("✓ Output is displayed");
        }

        /// <summary>
        /// Asserts that a specific output field matches the expected value.
        /// </summary>
        public void AssertOutputField(string fieldName, string expectedValue)
        {
            _logger.Information("Asserting {FieldName} = {ExpectedValue}", fieldName, expectedValue);
            
            var actualValue = _page.GetOutputValue(fieldName);
            
            Assert.That(actualValue, Is.EqualTo(expectedValue), 
                $"{fieldName} should match expected value");
            
            _logger.Information("✓ {FieldName} matches expected value", fieldName);
        }

        /// <summary>
        /// Asserts all form fields match expected values.
        /// </summary>
        public void AssertAllFields(string expectedName, string expectedEmail, 
            string expectedCurrentAddress, string expectedPermanentAddress)
        {
            _logger.Information("Asserting all output fields");
            
            Assert.Multiple(() =>
            {
                AssertOutputField("Name", expectedName);
                AssertOutputField("Email", expectedEmail);
                Assert.That(_page.GetOutputValue("Current Address"), Does.Contain(expectedCurrentAddress));
                Assert.That(_page.GetOutputValue("Permananet Address"), Does.Contain(expectedPermanentAddress)); // Note: typo in actual site
            });
            
            _logger.Information("✓ All fields match");
        }

        /// <summary>
        /// Asserts that output is empty (for negative tests).
        /// </summary>
        public void AssertOutputIsEmpty()
        {
            _logger.Information("Asserting output is empty");
            
            var outputs = _page.GetOutputs();
            Assert.That(outputs, Is.Empty, "Output should be empty");
            
            _logger.Information("✓ Output is empty");
        }
    }
}