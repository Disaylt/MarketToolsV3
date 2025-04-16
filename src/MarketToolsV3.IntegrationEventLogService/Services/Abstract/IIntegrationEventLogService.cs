using IntegrationEvents.Contract;
using MarketToolsV3.IntegrationEventLogService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketToolsV3.IntegrationEventLogService.Services.Abstract
{
    public interface IIntegrationEventLogService
    {
        Task SaveEventAsync(BaseIntegrationEvent @event, CancellationToken cancellationToken);
        Task<IReadOnlyCollection<IntegrationEventLogEntry>> GetNotPublishByTransaction(Guid transactionId, CancellationToken cancellationToken);
        Task MarkEventAsInProgressAsync(Guid eventId, CancellationToken cancellationToken);
        Task MarkEventAsPublishedAsync(Guid eventId, CancellationToken cancellationToken);
        Task MarkEventAsFailedAsync(Guid eventId, CancellationToken cancellationToken);
    }
}
