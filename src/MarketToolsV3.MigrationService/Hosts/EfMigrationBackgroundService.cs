using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketToolsV3.MigrationService.Services;
using Microsoft.EntityFrameworkCore;

namespace MarketToolsV3.MigrationService.Hosts
{
    internal class EfMigrationBackgroundService<T>(IServiceProvider serviceProvider,
        ILogger<EfMigrationBackgroundService<T>> logger,
        HostFinishService hostFinishService)
        : BackgroundService where T : DbContext
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            logger.LogInformation("Run migration for {type}", typeof(T).FullName);

            try
            {
                using var scope = serviceProvider.CreateScope();
                T context = scope.ServiceProvider.GetRequiredService<T>();
                await context.Database.MigrateAsync(stoppingToken);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Migration {type} failed", typeof(T).FullName);
            }
            finally
            {
                hostFinishService.MarkAsComplete();
            }

            logger.LogInformation("Migration completed for {type}", typeof(T).FullName);

        }
    }
}
