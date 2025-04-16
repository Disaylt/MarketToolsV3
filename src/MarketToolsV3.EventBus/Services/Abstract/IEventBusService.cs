using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketToolsV3.EventBus.Services.Abstract
{
    public interface IEventBusService
    {
        Task PublishAsync(object @event, Type type, CancellationToken cancellationToken);
    }
}
