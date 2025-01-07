using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketToolsV3.ConfigurationManager.Abstraction
{
    public interface IConfigManager
    {
        void JoinTo(IConfigurationManager applicationConfig);
    }
}
