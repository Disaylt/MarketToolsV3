using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Infrastructure.Services.Claims
{
    internal static class ClaimsExtensions
    {
        public static string? FindByType(this IEnumerable<Claim> claims, string type)
        {
            return claims.FirstOrDefault(x => x.Type == type)?.Value;
        }
    }
}
