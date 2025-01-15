using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketToolsV3.ConfigurationManager;

namespace MarketToolsV3.DbMigrations.Service.Extensions
{
    internal static class WbSellerCompaniesMigrationExtension
    {
        public static async Task AddWbSellerCompaniesMigration(this IHostApplicationBuilder builder)
        {
            ConfigurationServiceFactory configurationServiceFactory = new(builder.Configuration);
        }
    }
}
