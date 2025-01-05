using MarketToolsV3.ConfigurationManager.Abstraction;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketToolsV3.ConfigurationManager.Implementations
{
    internal class MarketToolsConfigurationBuilder(IConfigurationManager applicationConfig) 
        : ConfigurationBuilder
    {
        private readonly ConfigurationManagersFactory _configurationManagersFactory =
            new(applicationConfig);

        public async Task UploadAsync(string serviceName)
        {
            string? type = applicationConfig.GetValue<string>($"{serviceName}ConfigType")
                           ?? applicationConfig.GetValue<string>("ConfigType");

            if (string.IsNullOrEmpty(type) == false)
            {
                IConfigurationUploader configurationUploader = _configurationManagersFactory.Create(type);
                await configurationUploader.UploadAsync(this, serviceName);
            }
        }
    }
}
