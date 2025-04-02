using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntegrationEvents.Contract;
using MarketToolsV3.EventBus;

namespace MarketToolsV3.IntegrationEventLogServiceEf
{
    public class IntegrationEventLogService<TContext>(TContext context)
    where TContext : DbContext
    {
        public async Task SaveEventAsync(BaseIntegrationEvent @event, CancellationToken cancellationToken)
        {
            if (context.Database.CurrentTransaction is null)
            {
                throw new NullReferenceException("Transaction is null.");
            }

            IntegrationEventLogEntity logEvent = new(@event, context.Database.CurrentTransaction.TransactionId);
            await context.Set<IntegrationEventLogEntity>().AddAsync(logEvent, cancellationToken);

            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
