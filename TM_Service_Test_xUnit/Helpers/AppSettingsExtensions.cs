using System;
using Microsoft.Extensions.Configuration;

namespace Helpers
{
    public static class AppSettingsExtensions
    {
        public static IConfigurationRoot GetIConfigurationRoot(string outputPath)
        {
            return new ConfigurationBuilder()
                .SetBasePath(outputPath)
                .AddJsonFile("appsettings.json", optional: true)
                .AddEnvironmentVariables()
                .Build();
        }

        public static DataStore.DataConfig GetApplicationConfiguration(string outputPath)
        {
            var configuration = new DataStore.DataConfig();

            var iConfig = GetIConfigurationRoot(outputPath);

            iConfig
                .GetSection("Db")
                .Bind(configuration);

            return configuration;
        }
    }
}
