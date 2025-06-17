using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Identity.Domain.Seed;
using Identity.Infrastructure.Database;
using MarketToolsV3.ConfigurationManager;
using MarketToolsV3.ConfigurationManager.Abstraction;
using MarketToolsV3.ConfigurationManager.Models;
using MarketToolsV3.DbMigrations.Service.Workers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WB.Seller.Companies.Infrastructure.Database;

namespace MarketToolsV3.DbMigrations.Service.Extensions
{
    internal static class IdentityMigrationExtension
    {
        public static async Task AddIdentityMigration(this IHostApplicationBuilder builder)
        {
            ConfigurationServiceFactory configurationServiceFactory = new(builder.Configuration);
            ITypingConfigManager<ServicesAddressesConfig> addressesConfig = await configurationServiceFactory.CreateFromServicesAddressesAsync();

            ITypingConfigManager<ServiceConfiguration> serviceConfigManager =
                await configurationServiceFactory.CreateFromServiceAsync<ServiceConfiguration>(addressesConfig.Value.Identity.Name);

            builder.Services.AddDbContext<IdentityDbContext>(opt =>
            {
                opt.UseNpgsql(serviceConfigManager.Value.DatabaseConnection)
                    .UseSnakeCaseNamingConvention();
            });

            builder.Services
                .AddMigrationBuilderHostService<IdentityDbContext>()
                .AddHostService()
                .AddOptions(opt =>
                {
                    opt.Execute = context =>
                    {
                        context.Database.Migrate();
                        return Task.CompletedTask;
                    };
                });
        }
    }
}
