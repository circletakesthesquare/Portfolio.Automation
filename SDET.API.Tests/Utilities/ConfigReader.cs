using Microsoft.Extensions.Configuration;

namespace SDET.API.Tests.Utilities
{
    public static class ConfigReader
    {
        private static readonly IConfigurationRoot config =
            new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        public static string Get(string key)
        {
            return config[key] ?? throw new InvalidOperationException("BaseUrl not set");
        }
    }
}
