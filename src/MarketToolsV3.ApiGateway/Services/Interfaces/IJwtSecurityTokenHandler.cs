using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace MarketToolsV3.ApiGateway.Services.Interfaces
{
    public interface IJwtSecurityTokenHandler
    {
        JwtSecurityToken ReadJwtToken(string token);
        bool CanReadToken(string token);
        Task<TokenValidationResult> ValidateTokenAsync(string token, TokenValidationParameters validationParameters);
    }
}
