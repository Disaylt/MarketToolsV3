using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Identity.Domain.Constants;
using Identity.Domain.Seed;
using Identity.Infrastructure.Database;
using MarketToolsV3.ConfigurationManager;
using MarketToolsV3.ConfigurationManager.Abstraction;
using MarketToolsV3.DbMigrations.Service.Workers;
using Microsoft.Extensions.DependencyInjection;

namespace MarketToolsV3.DbMigrations.Service.Extensions
{
    internal static class IdentityMigrationExtension
    {
        public static async Task AddIdentityMigration(this IHostApplicationBuilder builder)
        {
            ConfigurationServiceFactory configurationServiceFactory = new(builder.Configuration);
            ITypingConfigManager<ServiceConfiguration> serviceConfigManager =
                await configurationServiceFactory.CreateFromServiceAsync<ServiceConfiguration>(IdentityConfig.ServiceName);

            builder.Services.AddNpgsql<IdentityDbContext>(serviceConfigManager.Value.DatabaseConnection);

            builder.Services.AddHostedService<EfCoreMigrationBackgroundService<IdentityDbContext>>();
        }
    }
}
