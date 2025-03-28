using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationEvents.Contract.Identity
{
    public record ModuleCreatedIntegrationEvent : BaseIntegrationEvent
    {
        public required string UserId { get; init; }
        public required string Path { get; init; }
        public int ModuleId { get; init; }
        public IReadOnlyCollection<string> Roles { get; init; } = [];
        public IReadOnlyDictionary<string, int> Claims { get; init; } = new Dictionary<string, int>();
    }
}
