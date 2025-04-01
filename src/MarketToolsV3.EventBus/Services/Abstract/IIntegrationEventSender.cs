using IntegrationEvents.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketToolsV3.EventBus.Services.Abstract
{
    public interface IIntegrationEventSender
    {
        Task SendAsync(BaseIntegrationEvent @event, CancellationToken cancellationToken);
    }
}
