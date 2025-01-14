using MarketToolsV3.DbMigrations.Service.Models;
using MarketToolsV3.DbMigrations.Service.Services.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketToolsV3.DbMigrations.Service.Services.Implementation
{
    internal class WorkNotificationServiceService : IWorkNotificationServiceService
    {
        private int _numCompletedTasks = 0;

        public event Action<int>? NotifiyTotalCompletedTask;

        public void MarkAsCompleted()
        {
            _numCompletedTasks += 1;
            NotifiyTotalCompletedTask?.Invoke(_numCompletedTasks);
        }
    }
}
