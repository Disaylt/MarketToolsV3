using MarketToolsV3.ConfigurationManager;
using MarketToolsV3.FakeData.WebApi.Domain.Seed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketToolsV3.ConfigurationManager.Abstraction;
using MarketToolsV3.FakeData.WebApi.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WB.Seller.Companies.Infrastructure.Database;

namespace MarketToolsV3.DbMigrations.Service.Extensions
{
    internal static class FakeDataMigrationExtension
    {
        public static async Task AddFakeDataCompaniesMigration(this IHostApplicationBuilder builder)
        {
            ConfigurationServiceFactory configurationServiceFactory = new(builder.Configuration);

            ITypingConfigManager<ServiceConfig> serviceConfigManager =
                await configurationServiceFactory.CreateFromServiceAsync<ServiceConfig>(FakeDataConfig.ServiceName);

            builder.Services.AddDbContext<FakeDataDbContext>(opt =>
            {
                opt.UseNpgsql(serviceConfigManager.Value.DatabaseConnection)
                    .UseSnakeCaseNamingConvention();
            });

            builder.Services
                .AddMigrationBuilderHostService<FakeDataDbContext>()
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
