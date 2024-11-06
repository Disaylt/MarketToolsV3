using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Models
{
    public record AuthDetailsDto
    {
        public required string SessionToken { get; init; }
        public required string AuthToken { get; init; }
    }
}
