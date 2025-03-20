using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Models
{
    public class ModuleAuthInfoDto
    {
        public required string Type { get; set; }
        public required string Path { get; set; }
        public int Id { get; set; }
        public IReadOnlyCollection<ModuleClaimDto> ClaimTypeAndValuePairs { get; set; } = [];
        public IReadOnlyCollection<string> Roles { get; set; } = [];
    }
}
