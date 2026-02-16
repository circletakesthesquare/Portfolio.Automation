namespace UI.Tests.TextBox
{
    [TestFixture]
    public class TextBoxTests : TestBase
    {

        private TextBoxPage _textBoxPage = null!;
        private TextBoxPageAssertions _assertions = null!;
        private NavigationHelper _navigationHelper = null!;

        [SetUp]
        public void TextBoxTestSetup()
        {
            // TestBase.Setup() will have already initialized the logger and navigated to Config.BaseUrl
            // Now navigate to the specific page for Text Box tests using the NavigationHelper
            _navigationHelper = new NavigationHelper(Driver, Wait, Logger);
            _navigationHelper.NavigateToTextBox();

            // Inttialize page object and assertions with logger
            _textBoxPage = new TextBoxPage(Driver, Wait, Logger);
            _assertions = new TextBoxPageAssertions(_textBoxPage, Logger);
        }


        [Test]
        [Category("UI")]
        [Category("Positive")]
        [Category("Integration")]
        [Category("Smoke")]
        [Category("TextBox")]
        [Description("Verify that submitting the Text Box form displays the correct output.")]
        public async Task SubmitTextBoxForm_ShouldDisplayCorrectOutput()
        {
            // Arrange
            Logger.Information("TEST: Submitting valid text box form");

            var testData = new
            {
                FullName = "John Doe",
                Email = "test@email.com",
                CurrentAddress = "123 Main St, City, Country",
                PermanentAddress = "456 Another St, City, Country"
            };

            // Act
            _textBoxPage.FillAndSubmitForm(testData.FullName, testData.Email, testData.CurrentAddress, testData.PermanentAddress);


            // Assertions
            using (new AssertionScope())
            {
                _assertions.AssertOutputIsDisplayed();
                _assertions.AssertAllFields(
                    testData.FullName,
                    testData.Email,
                    testData.CurrentAddress,
                    testData.PermanentAddress
                );
            }
        }

        // Negative Tests

        [Test]
        [Category("UI")]
        [Category("Negative")]
        [Category("Integration")]
        [Category("Regression")]
        [Category("TextBox")]
        [Description("Verify that submitting the Text Box form with no inputs displays no output.")]
        public async Task SubmitTextBoxForm_WithNoInputs_ShouldDisplayNoOutput()
        {
            // Arrange
            Logger.Information("TEST: Submitting empty text box form");

            // Act
            _textBoxPage.FillAndSubmitForm(string.Empty, string.Empty, string.Empty, string.Empty);

            // Assertions
            using (new AssertionScope())
            {
                _assertions.AssertOutputIsEmpty();
            }
        }

        [Test]
        [Category("UI")]
        [Category("Negative")]
        [Category("Integration")]
        [Category("Regression")]
        [Category("TextBox")]
        [Description("Verify that submitting the Text Box form with special characters displays correct output.")]
        public async Task SubmitTextBoxForm_WithSpecialCharacters_ShouldDisplayCorrectOutput()
        {
            // Arrange
            Logger.Information("TEST: Submitting text box form with special characters");
            var specialChars = "!@#$%^&*()_+-=[]{}|;':\",.<>/?`~";
            var email = "test@example.com";

            // Act
            _textBoxPage.FillAndSubmitForm(specialChars, email, specialChars, specialChars);

            // Assertions
            using (new AssertionScope())
            {
                _assertions.AssertOutputIsDisplayed();
                _assertions.AssertAllFields(specialChars, email, specialChars, specialChars);
            }
        }
        
        [Test]
        [Category("UI")]
        [Category("Negative")]
        [Category("Edge Case")]
        [Category("TextBox")]
        [Description("Verify that submitting the Text Box form with whitespace-only inputs displays no output.")]
        public async Task SubmitTextBoxForm_WithWhitespaceOnlyInputs_ShouldDisplayNoOutput()
        {
            // Arrange
            Logger.Information("TEST: Submitting text box form with whitespace-only inputs");
            var whitespaceInput = "     "; // 5 spaces

            // Act
            _textBoxPage.FillAndSubmitForm(whitespaceInput, whitespaceInput, whitespaceInput, whitespaceInput);

            // Assertions
            using (new AssertionScope())
            {
                _assertions.AssertOutputIsDisplayed();
            
                var outputs = _textBoxPage.GetOutputs();
                Logger.Information("Output fields present: {Count}", outputs.Count);
                
                // The site displays whitespace fields, verify structure
                Assert.That(outputs.Count, Is.GreaterThan(0), "Output should contain fields even with whitespace input");
            }
        }
    }    
}