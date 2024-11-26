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
        public string ElasticConnection { get; set; } = string.Empty;
        public string ElasticUser { get; set; } = string.Empty;
        public string ElasticPassword { get; set; } = string.Empty;
        public string RabbitMqConnection { get; set; } = string.Empty;
        public string RabbitMqLogin { get; set; } = string.Empty;
        public string RabbitMqPassword{ get; set; } = string.Empty;
    }
}
