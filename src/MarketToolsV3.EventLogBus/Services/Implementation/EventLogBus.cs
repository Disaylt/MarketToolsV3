using MarketToolsV3.EventBus.Services.Abstract;
using MarketToolsV3.IntegrationEventLogService.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketToolsV3.IntegrationEventLogService.Models;
using MarketToolsV3.EventLogBus.Services.Abstract;
using Microsoft.Extensions.Logging;

namespace MarketToolsV3.EventLogBus.Services.Implementation
{
    internal class EventLogBus(IIntegrationEventLogService integrationEventLogService,
        IIntegrationEventLogsConvertService integrationEventLogsConvertService,
        IEventBusService eventBusService,
        ILogger<EventLogBus> logger)
    : IEventLogBus
    {
        public async Task PublishNewByTransactionAsync(Guid transactionId, CancellationToken cancellationToken)
        {
            var logs = await integrationEventLogService
                .GetNotPublishByTransaction(transactionId, cancellationToken);

            foreach (var log in logs)
            {
                await PublishEventLogAsync(log, cancellationToken);
            }
        }

        public async Task PublishEventLogAsync(IntegrationEventLogEntry log, CancellationToken cancellationToken)
        {
            try
            {
                await integrationEventLogService.MarkEventAsInProgressAsync(log.Id, cancellationToken);
                var eventInfo = integrationEventLogsConvertService.Convert(log);
                await eventBusService.PublishAsync(eventInfo.Content, eventInfo.Type, cancellationToken);
                await integrationEventLogService.MarkEventAsPublishedAsync(log.Id, cancellationToken);
            }
            catch(Exception ex)
            {
                await integrationEventLogService.MarkEventAsFailedAsync(log.Id, cancellationToken);
                logger.LogError(ex, "Error publish event");
            }
        }
    }
}
