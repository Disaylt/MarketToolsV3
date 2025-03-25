using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using MarketToolsV3.ApiGateway.Extensions;
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
                Id = tokenData.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti)?.Value ?? null,
                SessionId = tokenData.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid)?.Value ?? null,
                ModuleAuthInfo = CreateModuleAuth(tokenData)
            };
        }

        private ModuleAuthInfoDto? CreateModuleAuth(JwtSecurityToken jwtSecurityToken)
        {
            string? moduleType = jwtSecurityToken.Claims.FindByType("moduleType");
            string? modulePath = jwtSecurityToken.Claims.FindByType("modulePath");

            if (int.TryParse(jwtSecurityToken.Claims.FindByType("moduleId"), out var moduleId)
                && moduleType != null
                && modulePath != null)
            {
                return new()
                {
                    Type = moduleType,
                    Id = moduleId,
                    Path = modulePath
                };
            }

            return null;
        }
    }
}
