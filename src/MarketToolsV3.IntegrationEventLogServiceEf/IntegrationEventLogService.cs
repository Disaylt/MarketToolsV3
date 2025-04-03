using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntegrationEvents.Contract;
using MarketToolsV3.EventBus.Models;
using MarketToolsV3.EventBus.Services.Abstract;

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

        public async Task<IReadOnlyCollection<IntegrationEventLogEntry>> GetNotPublishByTransaction(Guid transactionId)
        {
            return await _integrationEventLogRepository
                .Where(x => x.TransactionId == transactionId && x.State == EventStateEnum.NotPublished)
                .ToListAsync();
        }
    }
}
