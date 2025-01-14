using MarketToolsV3.DbMigrations.Service.Models;
using MarketToolsV3.DbMigrations.Service.Services.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketToolsV3.DbMigrations.Service.Workers
{
    internal class TestWorketService<T>(IWorkNotificationServiceService workNotificationServiceService)
        : MigrationBackgroundService(workNotificationServiceService)
    {

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.CompletedTask;
        }
    }
}
