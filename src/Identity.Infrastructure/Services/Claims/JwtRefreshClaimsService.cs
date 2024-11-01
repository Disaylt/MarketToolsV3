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
    internal class JwtRefreshClaimsService : IClaimsService<JwtRefreshTokenDto>
    {
        public IEnumerable<Claim> Create(JwtRefreshTokenDto details)
        {
            Claim jti = new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString());
            Claim iat = new Claim(JwtRegisteredClaimNames.Iat,
                EpochTime.GetIntDate(DateTime.UtcNow).ToString(CultureInfo.InvariantCulture),
                ClaimValueTypes.Integer64);
            Claim sessionId = new Claim(ClaimTypes.Sid, details.Id);

            List<Claim> claims = new List<Claim>
            {
                jti,
                iat,
                sessionId
            };

            return claims;
        }
    }
}
