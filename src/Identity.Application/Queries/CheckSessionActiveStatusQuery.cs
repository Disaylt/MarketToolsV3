using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Queries
{
    public class CheckSessionActiveStatusQuery : IRequest<bool>
    {
        public required string RefreshToken { get; set; }
    }
}
