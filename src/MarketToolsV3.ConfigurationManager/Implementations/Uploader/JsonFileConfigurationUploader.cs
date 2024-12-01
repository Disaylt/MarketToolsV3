using MarketToolsV3.ConfigurationManager.Abstraction;
using MarketToolsV3.ConfigurationManager.Utilities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketToolsV3.ConfigurationManager.Implementations.Uploader
{
    internal class JsonFileConfigurationUploader(IConfigurationManager configuration) 
        : IConfigurationUploader
    {
        public Task UploadAsync(IConfigurationBuilder builder, string serviceName)
        {
            string? filePath = configuration.GetValue<string>($"{serviceName}JsonBasePath")
                ?? configuration.GetValue<string>("JsonBasePath");

            if (string.IsNullOrEmpty(filePath))
            {
                return Task.CompletedTask;
            }

            builder.SetBasePath(filePath);

            string fileName = ConfigNameConverter.ConvertByConfigPattern(serviceName);
            builder.AddJsonFile($"{fileName}.json");

            return Task.CompletedTask;
        }
    }
}
