using FluentAssertions;
using FluentAssertions.Execution;
using NUnit.Framework;
using Portfolio.Automation.UI.Core;

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
    }    
}