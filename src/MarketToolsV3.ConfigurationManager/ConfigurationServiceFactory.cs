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
        public async Task<IConfigManager> CreateFromServiceAsync(string serviceName)
        {
            IConfigurationRoot configurationRoot = await CreateConfigurationRootAsync(serviceName);

            return new ConfigManager(configurationRoot);
        }

        public async Task<ITypingConfigManager<T>> CreateFromServiceAsync<T>(string serviceName) where T : class
        {
            IConfigurationRoot configurationRoot = await CreateConfigurationRootAsync(serviceName);

            return new TypingConfigManage<T>(configurationRoot);
        }

        public async Task<ITypingConfigManager<AuthConfig>> CreateFromAuthAsync()
        {
            return await CreateFromServiceAsync<AuthConfig>(ConfigurationNames.Auth);
        }

        public async Task<ITypingConfigManager<LoggingConfig>> CreateFromLoggingAsync()
        {
            return await CreateFromServiceAsync<LoggingConfig>(ConfigurationNames.Logging);
        }

        public async Task<ITypingConfigManager<ServicesAddressesConfig>> CreateFromServicesAddressesAsync()
        {
            return await CreateFromServiceAsync<ServicesAddressesConfig>(ConfigurationNames.ServiceAddresses);
        }

        public async Task<ITypingConfigManager<MessageBrokerConfig>> CreateFromMessageBrokerAsync()
        {
            return await CreateFromServiceAsync<MessageBrokerConfig>(ConfigurationNames.MessageBroker);
        }

        public async Task<ITypingConfigManager<TranslateCollectionConfig>> CreateFromTranslateCollectionAsync()
        {
            return await CreateFromServiceAsync<TranslateCollectionConfig>(ConfigurationNames.TranslateCollection);
        }

        private async Task<IConfigurationRoot> CreateConfigurationRootAsync(string serviceName)
        {
            MarketToolsConfigurationBuilder builder = new(applicationConfig);
            await builder.UploadAsync(serviceName);

            return builder.Build();
        }
    }
}
