using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketToolsV3.ConfigurationManager.Models
{
    public record MessageBrokerConfig
    {
        public string RabbitMqConnection { get; init; } = string.Empty;
        public string RabbitMqLogin { get; init; } = string.Empty;
        public string RabbitMqPassword { get; init; } = string.Empty;
    }
}
