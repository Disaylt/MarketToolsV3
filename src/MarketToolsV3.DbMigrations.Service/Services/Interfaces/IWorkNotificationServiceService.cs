using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketToolsV3.DbMigrations.Service.Services.Interfaces
{
    internal interface IWorkNotificationServiceService
    {
        void MarkAsCompleted();
        void Subscribe(Action<int> action);
    }
}
