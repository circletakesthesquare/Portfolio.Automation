namespace UI.Pages
{
    /// <summary>
    /// Page object model for the Text Box page.
    /// </summary>
    public class TextBoxPage : DecoratedBasePage
    {

        public TextBoxPage(IWebDriver driver, WebDriverWait wait, ILogger logger) : base(driver, wait, logger)
        {
            Logger.Debug("TextBoxPage initialized.");
        }

        private By FullNameInput => By.Id("userName");
        private By EmailInput => By.Id("userEmail");
        private By CurrentAddressInput => By.Id("currentAddress");
        private By PermanentAddressInput => By.Id("permanentAddress");
        private By SubmitButton => By.Id("submit");
        private By OutputSection => By.Id("output");

        // Page Actions
        public void FillForm(string fullName, string email, string currentAddress, string permanentAddress)
        {
            Logger.Information("Filling Text Box form with: FullName='{FullName}', Email='{Email}', CurrentAddress='{CurrentAddress}', PermanentAddress='{PermanentAddress}'",
             fullName, email, currentAddress, permanentAddress);

            EnterText(FullNameInput, fullName);
            EnterText(EmailInput, email);
            EnterText(CurrentAddressInput, currentAddress);
            EnterText(PermanentAddressInput, permanentAddress);

            Logger.Information("Form filled successfully.");
        }

        public void FillFullName(string fullName) => EnterText(FullNameInput, fullName);

        public void FillEmail(string email) => EnterText(EmailInput, email);

        public void FillCurrentAddress(string currentAddress) => EnterText(CurrentAddressInput, currentAddress);

        public void FillPermanentAddress(string address) => EnterText(PermanentAddressInput, address);


        public void SubmitForm()
        {
            Logger.Information("Submitting Text Box form.");
            ScrollToElement(SubmitButton);
            ClickElement(SubmitButton);
        }

        public void FillAndSubmitForm(string fullName, string email, string currentAddress, string permanentAddress)
        {
            FillForm(fullName, email, currentAddress, permanentAddress);
            SubmitForm();
        }

        // Verification Methods

        public bool IsOutputPopulated()
        {
            return IsActionSuccessful(
                OutputSection,
                element => element.Displayed && !string.IsNullOrEmpty(element.Text),
                timeoutSeconds: 5
            );
        }

        /// <summary>
        /// Retireves the output as a structured dictionary. The output is expected to be in the format of "Key:Value" pairs separated by new lines.
        /// </summary>
        /// <returns>Dictionary of output field names to values.</returns>
        public Dictionary<string, string> GetOutputs()
        {
            Logger.Debug("Parsing form outputs.");

            var outputData = new Dictionary<string, string>();

            if (!IsOutputPopulated())
            {
                Logger.Warning("Output section is not populated. Returning empty output.");
                return outputData; // Return empty if output is not populated
            }

            var outputElement = WaitForElementVisible(OutputSection);
            var outputText = outputElement.Text;

            Logger.Debug("Raw output text: {OutputText}", outputText);

            // Parse output text (format: "Key:Value")
            var lines = outputElement.Text.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var line in lines)
            {
                var parts = line.Split(new[] { ':' }, 2); // Split only on the first colon
                if (parts.Length == 2)
                {
                    var key = parts[0].Trim();
                    var value = parts[1].Trim();
                    outputData[key] = value;
                    Logger.Debug("Parsed output - Key: {Key}, Value: {Value}", key, value);
                }
                else
                {
                    Logger.Warning("Unable to parse line: {Line}", line);
                }
            }
            Logger.Information("Parsed {Count} output fields.", outputData.Count);
            return outputData;
        }

        public string GetOutputValue(string key)
        {
            var outputs = GetOutputs();
            return outputs.TryGetValue(key, out var value) ? value : null;
        }
    }
}