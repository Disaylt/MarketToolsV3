using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationEvents.Contract.Identity
{
    public record SessionCreatedIntegrationEvent : BaseIntegrationEvent
    {
        public required string SessionId { get; init; }
        public required string UserId { get; init; }
        public string? UserAgent { get; init; }
    }
}
