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
        public IReadOnlyDictionary<int, int> ClaimTypeAndValuePairs { get; set; } = new Dictionary<int, int>();
        public IReadOnlyCollection<string> Roles { get; set; } = [];
    }
}
