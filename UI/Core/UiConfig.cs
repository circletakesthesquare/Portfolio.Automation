using System.Text.Json;

namespace Portfolio.Automation.UI.Core
{
    public class UiConfig
    {
        public string BaseUrl { get; set; } = "https://demoqa.com";
        public string Browser { get; set; } = "Chrome";
        public bool Headless { get; set; } = false;
        public int DefaultTimeoutSeconds { get; set; } = 10;

        private const string ConfigFileName = "Config/appsettings.json";

        /// <summary>
        /// Loads the UI configuration from appsettings.json file, if it exists.
        /// </summary>
        /// <returns></returns>
        public static UiConfig Load()
        {
            if (!File.Exists(ConfigFileName)){
                // Create default config file if none found
                var defaultConfig = new UiConfig();

                var folder = Path.GetDirectoryName(ConfigFileName);
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder!);
                }

                var serializedConfig = JsonSerializer.Serialize(defaultConfig, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(ConfigFileName, serializedConfig);
                return defaultConfig;
            }

            var configJson = File.ReadAllText(ConfigFileName);
            return JsonSerializer.Deserialize<UiConfig>(configJson) ?? new UiConfig();
        }
    }
}
