using MarketToolsV3.DbMigrations.Service.Options;
using MarketToolsV3.DbMigrations.Service.Workers;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketToolsV3.DbMigrations.Service.Services.Interfaces;
using MarketToolsV3.DbMigrations.Service.Services.Implementation;

namespace MarketToolsV3.DbMigrations.Service.Utilities
{
    internal class MigrationBuilder<T>(IServiceCollection collection) : IMigrationBuilder<T> where T : class
    {
        public IMigrationBuilder<T> AddOptions(Action<MigrationOptions<T>> opt)
        {
            collection.Configure(opt);

            return this;
        }

        public IMigrationBuilder<T> AddService(Func<IServiceProvider, T> service)
        {
            collection.AddScoped(service);
            collection.AddScoped<IMigrationService<T>, MigrationService<T>>();

            return this;
        }

        public IMigrationBuilder<T> AddHostService()
        {
            collection.AddHostedService<SharedMigrationBackgroundService<T>>();

            return this;
        }
    }
}
