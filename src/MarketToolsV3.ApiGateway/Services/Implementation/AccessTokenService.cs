using MarketToolsV3.ApiGateway.Services.Interfaces;
using MarketToolsV3.ConfigurationManager.Models;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Text;

namespace MarketToolsV3.ApiGateway.Services.Implementation;

public class AccessTokenService(IJwtSecurityTokenHandler jwtSecurityTokenHandler,
    IOptions<AuthConfig> authOptions) 
    : IAccessTokenService
{
    private readonly AuthConfig _authConfig = authOptions.Value;

    public async Task<bool> IsValidAsync(string token)
    {
        HMACSHA256 hmac = new(Encoding.UTF8.GetBytes(_authConfig.AuthSecret));

        TokenValidationParameters tokenValidationParameters = new()
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(hmac.Key),
            ValidateIssuer = _authConfig.IsCheckValidIssuer,
            ValidateAudience = _authConfig.IsCheckValidAudience,
            ValidateLifetime = _authConfig.IsCheckExpireDate,
            ValidAudience = _authConfig.ValidAudience,
            ValidIssuer = _authConfig.ValidIssuer,
            ClockSkew = TimeSpan.Zero,
        };

        TokenValidationResult result = await jwtSecurityTokenHandler.ValidateTokenAsync(token, tokenValidationParameters);

        return result.IsValid;
    }
}