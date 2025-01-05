using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketToolsV3.ConfigurationManager.Abstraction;
using MarketToolsV3.ConfigurationManager.Constant;
using MarketToolsV3.ConfigurationManager.Implementations;
using MarketToolsV3.ConfigurationManager.Models;
using Microsoft.Extensions.Configuration;

namespace MarketToolsV3.ConfigurationManager
{
    public sealed class ConfigurationServiceFactory(IConfigurationManager applicationConfig)
    {
        private readonly ConfigurationManagersFactory _configurationManagersFactory =
        new(applicationConfig);

        public IConfigManager CreateFromService(string serviceName)
        {
            IConfigurationBuilder builder = new ConfigurationBuilder();

            string? type = applicationConfig.GetValue<string>($"{serviceName}ConfigType")
                           ?? applicationConfig.GetValue<string>("ConfigType");

            if (string.IsNullOrEmpty(type) == false)
            {
                IConfigurationUploader configurationUploader = _configurationManagersFactory.Create(type);
                configurationUploader.UploadAsync(builder, serviceName);
            }

            IConfigurationRoot configurationRoot = builder.Build();

            return new ConfigManager(configurationRoot);
        }

        public ITypingConfigManager<T> CreateFromService<T>(string serviceName) where T : class
        {
            IConfigurationBuilder builder = new ConfigurationBuilder();

            string? type = applicationConfig.GetValue<string>($"{serviceName}ConfigType")
                ?? applicationConfig.GetValue<string>("ConfigType");

            if (string.IsNullOrEmpty(type) == false)
            {
                IConfigurationUploader configurationUploader = _configurationManagersFactory.Create(type);
                configurationUploader.UploadAsync(builder, serviceName);
            }

            IConfigurationRoot configurationRoot = builder.Build();
            
            return new TypingConfigManage<T>(configurationRoot);
        }

        public ITypingConfigManager<AuthConfig> CreateFromAuth()
        {
            return CreateFromService<AuthConfig>(ConfigurationNames.Auth);
        }

        public ITypingConfigManager<LoggingConfig> CreateFromLogging()
        {
            return CreateFromService<LoggingConfig>(ConfigurationNames.Logging);
        }

        public ITypingConfigManager<MessageBrokerConfig> CreateFromMessageBroker()
        {
            return CreateFromService<MessageBrokerConfig>(ConfigurationNames.MessageBroker);
        }
    }
}
