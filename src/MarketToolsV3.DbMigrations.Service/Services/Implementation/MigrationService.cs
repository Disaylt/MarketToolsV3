using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketToolsV3.DbMigrations.Service.Options;
using MarketToolsV3.DbMigrations.Service.Services.Interfaces;
using MassTransit.Configuration;
using Microsoft.Extensions.Options;

namespace MarketToolsV3.DbMigrations.Service.Services.Implementation
{
    internal class MigrationService<T>(IOptions<MigrationOptions<T>> migrationOptions, T service)
        : IMigrationService<T> where T : class
    {
        public async Task Run()
        {
             var task = migrationOptions
                            .Value
                            .Execute 
                            ?? throw new NullReferenceException($"Migration task is null. Type: {typeof(T).FullName}");

             await task(service);
        }
    }
}