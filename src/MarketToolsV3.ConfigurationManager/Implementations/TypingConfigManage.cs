using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketToolsV3.ConfigurationManager.Abstraction;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MarketToolsV3.ConfigurationManager.Implementations
{
    internal class TypingConfigManage<T>(IConfigurationRoot configurationRoot)
        : ConfigManager(configurationRoot), ITypingConfigManager<T> where T : class
    {
        public T Value => ConfigurationRoot.Get<T>() 
                          ?? throw new NullReferenceException($"Configuration type:{nameof(T)} not found.");

        public void AddAsOptions(IServiceCollection services)
        {
            services.AddOptions<T>().Bind(ConfigurationRoot);
        }
    }
}
