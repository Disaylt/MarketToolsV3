using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Domain.Entities
{
    public class Module
    {
        public int Id { get; set; }
        public required string Path { get; set; }
        public int ExternalId { get; set; }
        public required string IdentityId { get; set; }
        public IdentityPerson Identity { get; set; } = null!;

        public List<ModuleClaim> Claims { get; set; } = [];
        public List<ModuleRole> Roles { get; set; } = [];
    }
}
