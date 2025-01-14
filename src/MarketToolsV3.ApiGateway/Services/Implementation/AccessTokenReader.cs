using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using MarketToolsV3.ApiGateway.Models.Tokens;
using MarketToolsV3.ApiGateway.Services.Interfaces;

namespace MarketToolsV3.ApiGateway.Services.Implementation
{
    public class AccessTokenReader(IJwtSecurityTokenHandler jwtSecurityTokenHandler)
        : ITokenReader<AccessToken>
    {
        public AccessToken? ReadOrDefault(string token)
        {
            if (jwtSecurityTokenHandler.CanReadToken(token) == false)
            {
                return null;
            }

            JwtSecurityToken tokenData = jwtSecurityTokenHandler.ReadJwtToken(token);

            return new AccessToken
            {
                SessionId = tokenData.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid)?.Value ?? null
            };
        }
    }
}
