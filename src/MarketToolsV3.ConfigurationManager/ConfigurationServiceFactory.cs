using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketToolsV3.ConfigurationManager.Abstraction;
using Microsoft.Extensions.Configuration;

namespace MarketToolsV3.ConfigurationManager
{
    public sealed class ConfigurationServiceFactory(IConfigurationManager applicationConfig)
    {
        private readonly ConfigurationManagersFactory _configurationManagersFactory =
            new(applicationConfig);

        public IConfigurationRoot CreateFromService(string serviceName)
        {
            IConfigurationBuilder builder = new ConfigurationBuilder();

            string? type = applicationConfig.GetValue<string>($"{serviceName}ConfigType")
                ?? applicationConfig.GetValue<string>("ConfigType");

            if (string.IsNullOrEmpty(type))
            {
                return builder.Build();
            }

            IConfigurationUploader configurationUploader = _configurationManagersFactory.Create(type);
            configurationUploader.UploadAsync(builder, serviceName);

            return builder.Build();
        }

        public 
    }
}
