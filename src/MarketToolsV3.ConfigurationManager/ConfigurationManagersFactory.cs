using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketToolsV3.ConfigurationManager.Abstraction;
using MarketToolsV3.ConfigurationManager.Implementations.Uploader;
using Microsoft.Extensions.Configuration;

namespace MarketToolsV3.ConfigurationManager
{
    public class ConfigurationManagersFactory(IConfigurationManager configurationManager)
    {
        public IConfigurationUploader Create(string type)
        {
            return type.ToLower() switch
            {
                "json" => new JsonFileConfigurationUploader(configurationManager),
                _ => throw new NullReferenceException($"Configuration manager for type:{type} not exists")
            };
        }
    }
}
