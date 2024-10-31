using MarketToolsV3.ConfigurationManager.Abstraction;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketToolsV3.ConfigurationManager.Implementations.Uploader
{
    internal class JsonFileConfigurationUploader(IConfigurationManager configuration) : IConfigurationUploader
    {
        public Task Upload()
        {
            string? filePath = configuration.GetValue<string>("MarketToolsV3ConfigFilePath");

            if (string.IsNullOrEmpty(filePath))
            {
                return Task.CompletedTask;
            }

            configuration.AddJsonFile(filePath);

            return Task.CompletedTask;
        }
    }
}
