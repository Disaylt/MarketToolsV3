﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketToolsV3.ConfigurationManager.Models
{
    public class ServicesAddressesConfig
    {
        public required ModuleConfig Identity { get; set; }
        public required ModuleConfig ApiGateway { get; set; }
        public required ModuleConfig UserNotifications { get; set; }
        public required CommonModule Common { get; set; }
        public required WbModule Wb { get; set; }
    }

    public class CommonModule
    {
        public required ModuleConfig Permissions { get; set; }
    }

    public class WbModule
    {
        public required WbSellerModule Seller { get; set; }
    }

    public class WbSellerModule
    {
        public required ModuleConfig Companies { get; set; }
        public required WbAutomationModule Automation { get; set; }
    }

    public class WbAutomationModule
    {
        public required ModuleConfig PriceManager { get; set; }
    }

    public class ModuleConfig
    {
        public required string Name { get; set; }
        public required AddressesConfig Addresses { get; set; }
    }

    public class AddressesConfig
    {

        public IReadOnlyList<string> Grpc { get; set; } = [];
        public IReadOnlyList<string> WebApi { get; set; } = [];

        public string? GetOrDefaultRandomGrpcAddress()
        {
            return GetRandomAddress(Grpc);
        }

        public string? GetOrDefaultRandomWebApiAddress()
        {
            return GetRandomAddress(WebApi);
        }

        private static string? GetRandomAddress(IReadOnlyList<string> addresses)
        {
            if (addresses.Count == 0) return null;
            int randomIndex = Random.Shared.Next(addresses.Count);

            return addresses[randomIndex];
        }
    }
}
