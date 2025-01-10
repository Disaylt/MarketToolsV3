using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Models
{
    public record LoginResponseDto
    {
        public required AuthDetailsDto AuthDetails { get; init; }
        public required IdentityDetailsDto IdentityDetails { get; init; }
    }
}
