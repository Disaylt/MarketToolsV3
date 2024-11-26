using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationEvents.Contract.Identity
{
    public record UserCreatedIntegrationEvent
    {
        public Guid CommandId { get; init; }
        public required string UserId { get; init; }
    }
}
