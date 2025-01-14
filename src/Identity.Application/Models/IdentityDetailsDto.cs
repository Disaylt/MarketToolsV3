using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Models
{
    public record IdentityDetailsDto
    {
        public required string Email { get; init; }
        public required string Name {get; init; }
        public required string Id { get; init; }
    }
}
