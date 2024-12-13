using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace MarketToolsV3.ConfigurationManager.Abstraction
{
    public interface IConfigurationUploader
    {
        Task UploadAsync(IConfigurationBuilder builder, string serviceName);
    }
}
