using IntegrationEvents.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketToolsV3.EventBus.Services.Abstract
{
    public interface IIntegrationEventLogService
    {
        Task SaveEventAsync(BaseIntegrationEvent @event, CancellationToken cancellationToken);
    }
}
