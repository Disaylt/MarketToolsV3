using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketToolsV3.ConfigurationManager
{
    public static class ConfigurationManagerExtension
    {
        public static async Task LoadConfigurationAsync(this IConfigurationManager configuration)
        {
            string? type = configuration.GetValue<string>("MarketToolsV3ConfigLoadType");

            if (string.IsNullOrEmpty(type))
            {
                return;
            }

            ConfigurationManagersFactory configurationManagersFactory =
                new ConfigurationManagersFactory(configuration);

            await configurationManagersFactory.Create(type).Upload();
        }
    }
}
