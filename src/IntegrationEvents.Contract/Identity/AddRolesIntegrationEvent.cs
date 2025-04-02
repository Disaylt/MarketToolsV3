using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationEvents.Contract.Identity
{
    public record AddRolesIntegrationEvent : BaseIntegrationEvent
    {
        public required string Path { get; init; }
        public int ModuleId { get; init; }
        public required IReadOnlyCollection<string> Roles { get; init; } = [];
    }
}
