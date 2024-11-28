using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace MarketToolsV3.ConfigurationManager
{
    public sealed class ConfigurationServiceFactory(IConfigurationManager applicationConfig)
    {
        public IConfigurationRoot CreateFromService(string serviceName)
        {
            IConfigurationBuilder builder = new ConfigurationBuilder();

            string? type = applicationConfig.GetValue<string>("MarketToolsV3ConfigType");

            if (string.IsNullOrEmpty(type))
            {
                return builder.Build();
            }

            ConfigurationManagersFactory configurationManagersFactory = new(configuration);
        }
    }
}
