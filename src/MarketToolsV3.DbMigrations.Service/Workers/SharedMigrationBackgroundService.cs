using MarketToolsV3.DbMigrations.Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketToolsV3.DbMigrations.Service.Workers
{
    internal class SharedMigrationBackgroundService<T>(IWorkNotificationServiceService notificationService,
        IServiceProvider serviceProvider,
        ILogger<SharedMigrationBackgroundService<T>> logger)
        : MigrationBackgroundService(notificationService)
        where T : class
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                logger.LogInformation("Run migration. Context: {typeContext}", typeof(T).FullName);

                await using var scope = serviceProvider.CreateAsyncScope();

                IMigrationService<T> migrationService = scope.ServiceProvider.GetRequiredService<IMigrationService<T>>();
                await migrationService.Run();

                logger.LogInformation("Migration for {typeContext} successfully completed", typeof(T).FullName);
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex, "Migration failed for {migration failed}", typeof(T).FullName);
            }
        }
    }
}
