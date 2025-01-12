using MarketToolsV3.ApiGateway.Models.Tokens;
using MarketToolsV3.ApiGateway.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MarketToolsV3.ApiGateway.Services.Implementation;

public class SessionTokenReader(IJwtSecurityTokenHandler jwtSecurityTokenHandler)
    : ITokenReader<SessionToken>
{
    public SessionToken? ReadOrDefault(string token)
    {
        if (jwtSecurityTokenHandler.CanReadToken(token) == false)
        {
            return null;
        }

        JwtSecurityToken tokenData = jwtSecurityTokenHandler.ReadJwtToken(token);

        return new SessionToken
        {
            Id = tokenData.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid)?.Value ?? null
        };
    }
}