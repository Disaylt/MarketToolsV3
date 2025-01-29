using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Infrastructure.Services.Abstract.Tokens
{
    internal interface IJwtSecurityTokenHandler
    {
        JwtSecurityToken ReadJwtToken(string token);
        Task<TokenValidationResult> ValidateTokenAsync(string token, TokenValidationParameters validationParameters);
    }
}
