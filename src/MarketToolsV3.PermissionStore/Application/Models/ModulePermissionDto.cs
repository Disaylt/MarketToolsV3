using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketToolsV3.PermissionStore.Application.Models
{
    public record ModulePermissionDto
    {
        public required string Path { get; init; }
        public required string ViewName { get; init; }
    }
}
