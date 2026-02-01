namespace UI.Tests.TextBox
{
    public class TextBoxTests : TestBase
    {

        [Test]
        [Category("UI")]
        [Category("Positive")]
        [Category("Integration")]
        [Category("Smoke")]
        [Category("TextBox")]
        [Description("Verify that submitting the Text Box form displays the correct output.")]
        public async Task SubmitTextBoxForm_ShouldDisplayCorrectOutput()
        {
            Driver.Navigate().GoToUrl($"{UiConfig.Load().BaseUrl}/text-box");
            var textBoxPage = new TextBoxPage(Driver, Wait);

            var fullName = "John Doe";
            var email = "test@email.com";
            var currentAddress = "123 Main St, City, Country";
            var permanentAddress = "456 Another St, City, Country";

            // Fill the form
            await textBoxPage.FillForm(fullName, email, currentAddress, permanentAddress);
            textBoxPage.SubmitForm();

            var outputs = textBoxPage.GetOutputs();


            // Assertions
            using (new AssertionScope())
            {
                outputs["Name"].Should().Be($"{fullName}");
                outputs["Email"].Should().Be($"{email}");
                outputs["Current Address"].Should().Be($"{currentAddress}");
                outputs["Permananet Address"].Should().Be($"{permanentAddress}"); // Note the key has a typo as per the actual output
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
            Driver.Navigate().GoToUrl($"{UiConfig.Load().BaseUrl}/text-box");
            var textBoxPage = new TextBoxPage(Driver, Wait);

            // Fill the form with empty strings
            await textBoxPage.FillForm(string.Empty, string.Empty, string.Empty, string.Empty);
            textBoxPage.SubmitForm();

            var outputs = textBoxPage.GetOutputs();

            // Assertions
            using (new AssertionScope())
            {
                outputs.ContainsKey("Name").Should().BeFalse("Expected no output for Name when input is empty");
                outputs.ContainsKey("Email").Should().BeFalse("Expected no output for Email when input is empty");
                outputs.ContainsKey("Current Address").Should().BeFalse("Expected no output for Current Address when input is empty");
                outputs.ContainsKey("Permananet Address").Should().BeFalse("Expected no output for Permananet Address when input is empty");
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
            Driver.Navigate().GoToUrl($"{UiConfig.Load().BaseUrl}/text-box");
            var textBoxPage = new TextBoxPage(Driver, Wait);

            var specialChars = "!@#$%^&*()_+-=[]{}|;':\",.<>/?`~";
            var email = "test@example.com";
            
            // Fill the form
            await textBoxPage.FillForm(specialChars, email, specialChars, specialChars);
            textBoxPage.SubmitForm();

            var outputs = textBoxPage.GetOutputs();

            // Assertions
            using (new AssertionScope())
            {
                outputs["Name"].Should().Be($"{specialChars}");
                outputs["Email"].Should().Be($"{email}");
                outputs["Current Address"].Should().Be($"{specialChars}");
                outputs["Permananet Address"].Should().Be($"{specialChars}");
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
            Driver.Navigate().GoToUrl($"{UiConfig.Load().BaseUrl}/text-box");
            var textBoxPage = new TextBoxPage(Driver, Wait);

            var whitespaceInput = "     "; // 5 spaces

            // Fill the form
            await textBoxPage.FillForm(whitespaceInput, whitespaceInput, whitespaceInput, whitespaceInput);
            textBoxPage.SubmitForm();

            var outputs = textBoxPage.GetOutputs();

            // Assertions
            using (new AssertionScope())
            {
                outputs["Name"].Should().BeNullOrEmpty("Expected no output for Name when input is whitespace only");
                outputs.ContainsKey("Email").Should().BeFalse("Expected no output for Email when input is whitespace only");
                outputs["Current Address"].Should().BeNullOrEmpty("Expected no output for Current Address when input is whitespace only");
                outputs["Permananet Address"].Should().BeNullOrEmpty("Expected no output for Permanent Address when input is whitespace only");
            }
        }
    }    
}