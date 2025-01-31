using MarketToolsV3.DbMigrations.Service.Models;
using MarketToolsV3.DbMigrations.Service.Services.Interfaces;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketToolsV3.DbMigrations.Service.Services.Implementation
{
    internal class WorkControlService : IWorkControlService
    {
        private readonly IHostApplicationLifetime _hostApplicationLifetime;
        private readonly ServiceConfig _serviceConfig;

        public WorkControlService(IWorkNotificationServiceService workNotificationServiceService,
            IHostApplicationLifetime lifetime,
            IOptions<ServiceConfig> options)
        {
            _serviceConfig = options.Value;
            _hostApplicationLifetime = lifetime;
            workNotificationServiceService.NotifyTotalCompletedTask += ExitServiceBeforeCompleteAllTask;
        }

        private void ExitServiceBeforeCompleteAllTask(int quantityTasks)
        {
            if(quantityTasks >= _serviceConfig.RegisteredNumberTasks)
            {
                _hostApplicationLifetime.StopApplication();
            }
        }
    }
}
