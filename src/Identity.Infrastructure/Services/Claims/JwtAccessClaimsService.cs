using Identity.Application.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Infrastructure.Services.Claims
{
    internal class JwtAccessClaimsService(IRolesClaimServices rolesClaimServices)
        : IClaimsService<JwtAccessTokenDto>
    {
        public IEnumerable<Claim> Create(JwtAccessTokenDto details)
        {
            Claim jti = new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString());
            Claim iat = new Claim(JwtRegisteredClaimNames.Iat,
                EpochTime.GetIntDate(DateTime.UtcNow).ToString(CultureInfo.InvariantCulture),
                ClaimValueTypes.Integer64);
            Claim id = new Claim(ClaimTypes.NameIdentifier, details.UserId);

            List<Claim> claims =
            [
                jti,
                iat,
                id
            ];

            IEnumerable<Claim> roles = rolesClaimServices.Create(details.Roles);
            claims.AddRange(roles);

            return claims;
        }
    }
}
