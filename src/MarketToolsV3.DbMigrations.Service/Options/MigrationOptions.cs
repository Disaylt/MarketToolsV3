using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketToolsV3.DbMigrations.Service.Options
{
    public class MigrationOptions<T> where T : class
    {
        public Func<T, Task>? Execute { get; set; }
    }
}
