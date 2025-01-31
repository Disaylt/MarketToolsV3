using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketToolsV3.DbMigrations.Service.Options;

namespace MarketToolsV3.DbMigrations.Service.Utilities
{
    internal interface IMigrationBuilder<T> where T : class
    {
        IMigrationBuilder<T> AddOptions(Action<MigrationOptions<T>> opt);
        IMigrationBuilder<T> AddService(Func<IServiceProvider, T> service);
        IMigrationBuilder<T> AddHostService();
    }
}
