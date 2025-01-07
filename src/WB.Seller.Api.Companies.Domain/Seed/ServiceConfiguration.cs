using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WB.Seller.Api.Companies.Domain.Seed
{
    public class ServiceConfiguration
    {
        public string DatabaseConnection { get; set; } = string.Empty;
        public string RedisConnection { get; set; } = string.Empty;
    }
}
