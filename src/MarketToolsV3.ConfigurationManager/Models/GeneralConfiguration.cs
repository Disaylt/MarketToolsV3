using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketToolsV3.ConfigurationManager.Models
{
    public class GeneralConfiguration
    {
        public string AuthSecret { get; set; } = string.Empty;
        public string ElasticUrl { get; set; } = string.Empty;
        public string ElasticUser { get; set; } = string.Empty;
        public string ElasticPassword { get; set; } = string.Empty;
    }
}
