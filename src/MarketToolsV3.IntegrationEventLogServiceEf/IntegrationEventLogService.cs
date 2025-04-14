using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntegrationEvents.Contract;
using MarketToolsV3.IntegrationEventLogService.Models;
using MarketToolsV3.IntegrationEventLogService.Services.Abstract;

namespace MarketToolsV3.IntegrationEventLogServiceEf
{
    public class IntegrationEventLogService<TContext>(TContext context) : IIntegrationEventLogService
    where TContext : DbContext
    {
        private readonly DbSet<IntegrationEventLogEntry> _integrationEventLogRepository =
            context.Set<IntegrationEventLogEntry>();

        public async Task SaveEventAsync(BaseIntegrationEvent @event, CancellationToken cancellationToken)
        {
            if (context.Database.CurrentTransaction is null)
            {
                throw new NullReferenceException("Transaction is null.");
            }

            IntegrationEventLogEntry logEvent = new(@event, context.Database.CurrentTransaction.TransactionId);

            await _integrationEventLogRepository
                .AddAsync(logEvent, cancellationToken);
        }

        public async Task<IReadOnlyCollection<IntegrationEventLogEntry>> GetNotPublishByTransaction(Guid transactionId, CancellationToken cancellationToken)
        {
            return await _integrationEventLogRepository
                .Where(x => x.TransactionId == transactionId && x.State == EventStateEnum.NotPublished)
                .ToListAsync(cancellationToken);
        }

        public async Task MarkEventAsInProgressAsync(Guid eventId, CancellationToken cancellationToken)
        {
            await UpdateEventStatusAsync(eventId, EventStateEnum.InProcess, cancellationToken);
        }

        public async Task MarkEventAsPublishedAsync(Guid eventId, CancellationToken cancellationToken)
        {
            await UpdateEventStatusAsync(eventId, EventStateEnum.Complete, cancellationToken);
        }

        public async Task MarkEventAsFailedAsync(Guid eventId, CancellationToken cancellationToken)
        {
            await UpdateEventStatusAsync(eventId, EventStateEnum.Error, cancellationToken);
        }

        private async Task UpdateEventStatusAsync(Guid eventId, 
            EventStateEnum state,
            CancellationToken cancellationToken)
        {
            var eventLog = await _integrationEventLogRepository.FindAsync([eventId], cancellationToken: cancellationToken)
                           ?? throw new NullReferenceException("Event not found!");
            eventLog.State = state;

            if (state == EventStateEnum.InProcess)
            {
                eventLog.TimeSent++;
            }

            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
