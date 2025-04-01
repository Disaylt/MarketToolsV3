using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntegrationEvents.Contract;
using MarketToolsV3.EventBus.Services.Abstract;
using MassTransit;

namespace MarketToolsV3.EventBus.Services.Implementation
{
    public class MassTransitIntegrationEventSender<TEvent>(IBus eventBus)
        : IIntegrationEventSender
        where TEvent : BaseIntegrationEvent
    {
        public async Task SendAsync(BaseIntegrationEvent @event, CancellationToken cancellationToken)
        {
            if (@event is TEvent typeEvent)
            {
                await eventBus.Publish(typeEvent, cancellationToken);
            }
            else
            {
                throw new Exception("Integration type not supported.");
            }
        }
    }
}
