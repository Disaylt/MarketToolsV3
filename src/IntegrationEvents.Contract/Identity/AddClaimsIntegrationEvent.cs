using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationEvents.Contract.Identity
{
    public record AddClaimsIntegrationEvent : BaseIntegrationEvent
    {
        public required string Path { get; init; }
        public int ModuleId { get; init; }
        public IReadOnlyDictionary<string, int> Claims { get; init; } = new Dictionary<string, int>();
    }
}
