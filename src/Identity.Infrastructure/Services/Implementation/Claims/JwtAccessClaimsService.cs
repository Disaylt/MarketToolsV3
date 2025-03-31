using Identity.Application.Models;
using Identity.Infrastructure.Services.Abstract;
using Identity.Infrastructure.Services.Abstract.Claims;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Identity.Infrastructure.Services.Implementation.Claims
{
    public class JwtAccessClaimsService(IRolesClaimService rolesClaimService)
        : IClaimsService<JwtAccessTokenDto>
    {
        public IEnumerable<Claim> Create(JwtAccessTokenDto details)
        {
            Claim jti = new(JwtRegisteredClaimNames.Jti, details.Id);
            Claim iat = new(JwtRegisteredClaimNames.Iat,
                EpochTime.GetIntDate(DateTime.UtcNow).ToString(CultureInfo.InvariantCulture),
                ClaimValueTypes.Integer64);
            Claim sessionId = new(ClaimTypes.Sid, details.SessionId);
            Claim userId = new(ClaimTypes.NameIdentifier, details.UserId);

            List<Claim> claims =
            [
                jti,
                iat,
                userId,
                sessionId
            ];

            if (details.ModuleAuthInfo is not null)
            {

                string moduleRoleType = $"module_{ClaimTypes.Role}";

                foreach (var role in details.ModuleAuthInfo.Roles)
                {
                    claims.Add(new Claim(moduleRoleType, role));
                }

                foreach (var typeAndValue in details.ModuleAuthInfo.ClaimTypeAndValuePairs)
                {
                    claims.Add(new Claim($"mp_{typeAndValue.Type}", typeAndValue.Value.ToString()));
                }

                claims.Add(new Claim("moduleType", details.ModuleAuthInfo.Type));
                claims.Add(new Claim("modulePath", details.ModuleAuthInfo.Path));
                claims.Add(new Claim("moduleId", details.ModuleAuthInfo.Id.ToString()));
            }

            IEnumerable<Claim> roles = rolesClaimService.Create(details.Roles);
            claims.AddRange(roles);

            return claims;
        }
    }
}
