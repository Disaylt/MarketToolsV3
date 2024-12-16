using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketToolsV3.MigrationService.Services
{
    internal interface IMigrationTaskService
    {
        public event Action? CompletedAllTask;
        public void MarkAsComplete();
    }
}
