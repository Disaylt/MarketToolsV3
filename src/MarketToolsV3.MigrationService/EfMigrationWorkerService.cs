using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MarketToolsV3.MigrationService
{
    internal class EfMigrationWorkerService<T>(IServiceProvider serviceProvider,
        ILogger logger) 
        : BackgroundService where T : DbContext
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            logger.LogInformation("Run migration for {type}", nameof(T));

            try
            {
                using var scope = serviceProvider.CreateScope();
                T context = scope.ServiceProvider.GetRequiredService<T>();
                await context.Database.MigrateAsync(stoppingToken);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Migration {type} failed", nameof(T));
            }
        }
    }
}
