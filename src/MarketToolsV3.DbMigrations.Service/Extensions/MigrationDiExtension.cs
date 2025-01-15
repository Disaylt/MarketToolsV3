using MarketToolsV3.DbMigrations.Service.Attributes;
using MarketToolsV3.DbMigrations.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketToolsV3.DbMigrations.Service.Extensions
{
    internal static class MigrationDiExtension
    {
        public static void DetermineTotalMigrationServices(this IHostApplicationBuilder hostApplicationBuilder)
        {
            int total = hostApplicationBuilder.Services.Count(x =>
            {
                return x.ServiceType == typeof(IHostedService)
                    && x.ImplementationType != null
                    && x.ImplementationType.GetCustomAttributes(true).Any(attr => attr.GetType() == typeof(RegisterMigrationHostServiceAttribute));
            });
            hostApplicationBuilder.Services.AddOptions<ServiceConfig>().Configure(x => x.RegisteredNumberTasks = total);
        }
    }
}
