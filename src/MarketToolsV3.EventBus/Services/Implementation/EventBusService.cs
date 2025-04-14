using MarketToolsV3.EventBus.Services.Abstract;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketToolsV3.EventBus.Services.Implementation
{
    internal class EventBusService(IBus bus)
        : IEventBusService
    {
        public async Task PublishAsync(object @event, Type type, CancellationToken cancellationToken)
        {
            await bus.Publish(@event, type, cancellationToken);
        }
    }
}
