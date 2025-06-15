using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WB.Seller.Companies.Application.Models
{
    public record ModulePermissionInfoDto
    {
        public required string Path { get; init; }
        public required string View { get; init; }
    }
}
