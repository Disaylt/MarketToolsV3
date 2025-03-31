using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Models
{
    public class ModuleClaimDto
    {
        public required string Type { get; set; }
        public int Value { get; set; }
    }
}
