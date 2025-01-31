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
    internal class MigrationBuilder<T> : IMigrationBuilder<T> where T : class
    {
        private readonly IServiceCollection _collection;
        public MigrationBuilder(IServiceCollection collection)
        {
            _collection = collection;

            _collection.AddScoped<IMigrationService<T>, MigrationService<T>>();
        }

        public IMigrationBuilder<T> AddOptions(Action<MigrationOptions<T>> opt)
        {
            _collection.Configure(opt);

            return this;
        }

        public IMigrationBuilder<T> AddService(Func<IServiceProvider, T> service)
        {
            _collection.AddScoped(service);

            return this;
        }

        public IMigrationBuilder<T> AddHostService()
        {
            _collection.AddHostedService<SharedMigrationBackgroundService<T>>();

            return this;
        }
    }
}
