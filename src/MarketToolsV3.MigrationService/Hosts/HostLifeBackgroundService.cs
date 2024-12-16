using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketToolsV3.MigrationService.Services;

namespace MarketToolsV3.MigrationService.Hosts
{
    internal class HostLifeBackgroundService(IMigrationTaskService migrationTaskService,
        IHostApplicationLifetime hostApplicationLifetime) 
        : BackgroundService
    {
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            migrationTaskService.CompletedAllTask += hostApplicationLifetime.StopApplication;

            return Task.CompletedTask;
        }
    }
}
