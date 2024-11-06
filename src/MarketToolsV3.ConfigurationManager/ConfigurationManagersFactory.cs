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
        private readonly Dictionary<string, IConfigurationUploader> _typeAndUploaderPair = new()
        {
            {"JsonFile", new JsonFileConfigurationUploader(configurationManager)}
        };

        public IConfigurationUploader Create(string type)
        {
            return _typeAndUploaderPair[type];
        }
    }
}
