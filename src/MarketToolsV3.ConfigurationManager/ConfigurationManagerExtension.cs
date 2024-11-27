using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketToolsV3.ConfigurationManager.Models;

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

            ConfigurationManagersFactory configurationManagersFactory = new(configuration);

            await configurationManagersFactory.Create(type).Upload();
        }

        public static GlobalConfiguration<T> GetGlobalConfig<T>(this IConfigurationManager configuration, string serviceName)
            where T : class, new()
        {
            return new GlobalConfiguration<T>
            {
                General = configuration.GetSection("General").Get<GeneralConfiguration>() ?? new GeneralConfiguration(),
                Service = configuration.GetSection(serviceName).Get<T>() ?? new T(),
            };
        }
    }
}
