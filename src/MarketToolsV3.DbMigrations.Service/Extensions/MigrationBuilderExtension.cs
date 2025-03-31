using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketToolsV3.DbMigrations.Service.Utilities;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;

namespace MarketToolsV3.DbMigrations.Service.Extensions
{
    internal static class MigrationBuilderExtension
    {
        public static IMigrationBuilder<T> AddMigrationBuilderHostService<T>(this IServiceCollection collection)
        where T : class
        {
            IMigrationBuilder<T> migrationBuilder = new MigrationBuilder<T>(collection);

            return migrationBuilder;
        }
    }
}
