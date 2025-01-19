using MarketToolsV3.DbMigrations.Service.Attributes;
using MarketToolsV3.DbMigrations.Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketToolsV3.DbMigrations.Service.Workers
{
    [RegisterMigrationHostService]
    internal abstract class MigrationBackgroundService(IWorkNotificationServiceService workNotificationServiceService) 
        : BackgroundService
    {
        public override Task? ExecuteTask => base.ExecuteTask?
            .ContinueWith(x=>
            {
                workNotificationServiceService.MarkAsCompleted();
            });
    }
}
