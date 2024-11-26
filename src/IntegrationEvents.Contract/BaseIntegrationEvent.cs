using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationEvents.Contract
{
    public record BaseIntegrationEvent
    {
        public Guid Id { get; init; }
    }
}
