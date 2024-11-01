using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Infrastructure.Services.Tokens
{
    internal interface IJwtTokenService
    {
        public SigningCredentials CreateSigningCredentials(string secret);
        public JwtSecurityToken ReadJwtToken(string token);
        public Task<TokenValidationResult> GetValidationResultAsync
        (string token,
            string secret,
            bool checkIssuerSigningKey = true,
            bool checkValidateIssuer = true,
            bool checkValidateAudience = true,
            bool checkValidateLifetime = true);
    }
}
