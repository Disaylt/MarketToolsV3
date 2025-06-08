using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketToolsV3.ConfigurationManager.Models
{
    public class ServicesAddressesConfig
    {
        public AddressConfig Identity { get; set; } = new();
        public AddressConfig ApiGateway { get; set; } = new();
        public CommonModule Common { get; set; } = new();
    }

    public class CommonModule
    {
        public AddressConfig Permissions { get; set; } = new();
    }

    public class AddressConfig
    {
        public string? Grpc { get; set; }
        public string? WebApi { get; set; }
    }
}
