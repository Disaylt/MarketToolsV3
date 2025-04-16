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
            var publishTask = bus.Publish(@event, type, cancellationToken);
            var timeoutTask = Task.Delay(TimeSpan.FromSeconds(5), cancellationToken);

            var completedTask = await Task.WhenAny(publishTask, timeoutTask);

            if (completedTask == timeoutTask)
            {
                throw new TimeoutException("Event publication time has expired.");
            }

            await publishTask;
        }
    }
}
