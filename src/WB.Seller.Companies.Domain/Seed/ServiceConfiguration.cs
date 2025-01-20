using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WB.Seller.Companies.Domain.Seed
{
    public class ServiceConfiguration
    {
        public string DatabaseConnection { get; set; } = string.Empty;
        public RedisConfig RedisConfig { get; set; } = new();
    }

    public class RedisConfig
    {
        public string? Host { get; set; }
        public int Port { get; set; }
        public string? Password { get; set; }
        public string? User { get; set; }
        public int Database { get; set; }

    }
}
