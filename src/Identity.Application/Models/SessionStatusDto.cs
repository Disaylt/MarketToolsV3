using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Models
{
    public record SessionStatusDto
    {
        public required string Id { get; init; }
        public bool IsActive { get; set; }
    }
}
