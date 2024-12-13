using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketToolsV3.ConfigurationManager.Utilities
{
    internal class ConfigNameConverter
    {
        public static string ConvertByConfigPattern(string serviceName)
        {
            return $"{serviceName}-config";
        }
    }
}
