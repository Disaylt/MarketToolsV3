using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Identity.Application.Models;
using Identity.Domain.Entities;
using Identity.Domain.Seed;
using MediatR;
using Microsoft.Extensions.Options;

namespace Identity.Application.Queries
{
    public class GetSessionDetailsQuery : IRequest<SessionStatusDto>
    {
        public required string Id { get; set; }
    }
}
