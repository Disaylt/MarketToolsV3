using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketToolsV3.DbMigrations.Service.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MarketToolsV3.DbMigrations.Service.Workers
{
    internal class EfCoreMigrationBackgroundService<T>(IWorkNotificationServiceService notificationService,
        IServiceProvider serviceProvider,
        ILogger<EfCoreMigrationBackgroundService<T>> logger)
    : MigrationBackgroundService(notificationService)
    where T : DbContext
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            try
            {
                logger.LogInformation("Run ef core migration. Context: {typeContext}" , typeof(T).FullName);

                await using var scope = serviceProvider.CreateAsyncScope();

                T context = scope.ServiceProvider.GetRequiredService<T>();
                await context.Database.MigrateAsync(stoppingToken);

                logger.LogInformation("Migration for {typeContext} successfully completed", typeof(T).FullName);
            }
            catch(Exception ex)
            {
                logger.LogCritical(ex, "migration failed for {migration failed}", typeof(T).FullName);
            }
        }
    }
}
