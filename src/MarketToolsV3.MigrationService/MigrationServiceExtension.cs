using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketToolsV3.MigrationService
{
    public static class MigrationServiceExtension
    {
        public static void AddMigrationHostService<T>(this HostApplicationBuilder builder) where T : BackgroundService
        {
            builder.Services.AddHostedService<T>();
            builder.Services.Configure<HostCounterConfig>(opt => opt.Quantity+= 1);
        }
    }
}
