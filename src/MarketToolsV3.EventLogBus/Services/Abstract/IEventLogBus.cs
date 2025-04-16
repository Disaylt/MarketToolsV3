using MarketToolsV3.IntegrationEventLogService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketToolsV3.EventLogBus.Services.Abstract
{
    public interface IEventLogBus
    {
        Task PublishNewByTransactionAsync(Guid transactionId, CancellationToken cancellationToken);
        Task PublishEventLogAsync(IntegrationEventLogEntry log, CancellationToken cancellationToken);
    }
}
