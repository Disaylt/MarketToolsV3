using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Infrastructure.Services
{
    internal interface IRolesClaimService
    {
        IEnumerable<Claim> Create(IEnumerable<string> roles);
    }
}
