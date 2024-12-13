using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketToolsV3.ConfigurationManager.Models
{
    public record LoggingConfig
    {
        public string ElasticConnection { get; init; } = string.Empty;
        public string ElasticUser { get; init; } = string.Empty;
        public string ElasticPassword { get; init; } = string.Empty;
    }
}
