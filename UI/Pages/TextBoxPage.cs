
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using UI.Pages;

namespace UI
{
    /// <summary>
    /// Page object model for the Text Box page.
    /// </summary>
    public class TextBoxPage : BasePage
    {

        public TextBoxPage(IWebDriver driver, WebDriverWait wait) : base(driver, wait){}

        private By FullNameInput => By.Id("userName");
        private By EmailInput => By.Id("userEmail");
        private By CurrentAddressInput => By.Id("currentAddress");
        private By PermanentAddressInput => By.Id("permanentAddress");
        private By SubmitButton => By.Id("submit");
        private By OutputSection => By.Id("output");

        public async Task FillForm(string fullName, string email, string currentAddress, string permanentAddress)
        {
            EnterText(FullNameInput, fullName);
            EnterText(EmailInput, email);
            EnterText(CurrentAddressInput, currentAddress);
            EnterText(PermanentAddressInput, permanentAddress);
        }

        public void SubmitForm()
        {
            ClickElement(SubmitButton);
        }

        public Dictionary<string, string> GetOutputs()
        {
            var outputData = new Dictionary<string, string>();

            var outputElement = WaitForElementVisible(OutputSection);

            // The output is structured as:
            // Name:John Doe
            var lines = outputElement.Text.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var line in lines)
            {
                var parts = line.Split(new[] { ':' }, 2); // Split only on the first colon
                if (parts.Length == 2)
                {
                    outputData[parts[0].Trim()] = parts[1].Trim(); // Add the new key-value pair
                }
            }
            return outputData;
        }
    }
}