using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Domain.Entities
{
    public class ModuleRole
    {
        public required string Value { get; set; }
        public int ModuleId { get; set; }
        public Module Module { get; set; } = null!;
    }
}
