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
        public string LogElasticConnection { get; set; } = string.Empty;
        public string LogElasticUser { get; set; } = string.Empty;
        public string LogElasticPassword { get; set; } = string.Empty;
        public string MessageBrokerRabbitMqConnection { get; set; } = string.Empty;
        public string MessageBrokerRabbitMqLogin { get; set; } = string.Empty;
        public string MessageBrokerRabbitMqPassword { get; set; } = string.Empty;
    }
}
