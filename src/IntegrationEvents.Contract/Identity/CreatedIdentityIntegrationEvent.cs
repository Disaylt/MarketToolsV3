using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationEvents.Contract.Identity
{
    public record CreatedIdentityIntegrationEvent : BaseIntegrationEvent
    {
        public required string IdentityId { get; init; }
    }
}
