using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Infrastructure.Services
{
    internal class RolesClaimService : IRolesClaimService
    {
        public virtual IEnumerable<Claim> Create(IEnumerable<string> roles)
        {
            return roles
                .Distinct()
                .Select(r => new Claim(ClaimTypes.Role, r));
        }
    }
}
