using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketToolsV3.DbMigrations.Service.Options;
using MarketToolsV3.DbMigrations.Service.Services.Interfaces;

namespace MarketToolsV3.DbMigrations.Service.Services.Implementation
{
    internal class MigrationService<T>(MigrationOptions<T> migrationOptions)
        : IMigrationService<T> where T : class
    {
        public async Task Run()
        {
            await migrationOptions.Execute();
        }
    }
}
