using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Models
{
    public record AuthInfoDto
    {
        public bool IsValid { get; init; }
        public bool Refreshed { get; init; }
        public AuthDetailsDto? Details { get; init; }
    }
}
