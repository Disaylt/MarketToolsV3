using Identity.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Queries
{
    public class GetActiveSessionsQuery : IRequest<IEnumerable<SessionDto>>
    {
        public string? CurrentSessionId { get; set; }
        public required string UserId { get; set; }
    }
}
