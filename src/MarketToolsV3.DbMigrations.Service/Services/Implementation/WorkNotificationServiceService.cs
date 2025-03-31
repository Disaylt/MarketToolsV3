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

        private event Action<int>? NotifyTotalCompletedTask;

        public void Subscribe(Action<int> action)
        {
            NotifyTotalCompletedTask += action;
        }

        public void MarkAsCompleted()
        {
            _numCompletedTasks += 1;
            NotifyTotalCompletedTask?.Invoke(_numCompletedTasks);
        }
    }
}
