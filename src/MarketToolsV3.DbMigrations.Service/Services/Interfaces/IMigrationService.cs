using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketToolsV3.DbMigrations.Service.Services.Interfaces
{
    public interface IMigrationService<T> where T : class
    {
        Task Run();
    }
}
