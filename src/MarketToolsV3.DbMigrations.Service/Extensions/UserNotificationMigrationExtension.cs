using MarketToolsV3.ConfigurationManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketToolsV3.ConfigurationManager.Abstraction;
using MassTransit.Configuration;
using UserNotifications.Domain.Seed;
using UserNotifications.Infrastructure.Database;
using Microsoft.Extensions.Options;

namespace MarketToolsV3.DbMigrations.Service.Extensions
{
    public static class UserNotificationMigrationExtension
    {
        public static async Task AddUserNotificationsMigrations(this IHostApplicationBuilder builder)
        {
            ConfigurationServiceFactory configurationServiceFactory = new(builder.Configuration);

            ITypingConfigManager<ServiceConfiguration> serviceConfigManager = await configurationServiceFactory
                    .CreateFromServiceAsync<ServiceConfiguration>(ServiceConstants.ServiceName);
            serviceConfigManager.AddAsOptions(builder.Services);

            builder.Services
                .AddMigrationBuilderHostService<MongoDatabaseBuilder>()
                .AddOptions(opt =>
                {
                    opt.Execute = async databaseBuilder => await databaseBuilder.Build();
                })
                .AddService(x =>
                {
                    IOptions<ServiceConfiguration> configuration = x.GetRequiredService<IOptions<ServiceConfiguration>>();
                    return new MongoDatabaseBuilder(configuration.Value);
                })
                .AddHostService();
        }
    }
}
