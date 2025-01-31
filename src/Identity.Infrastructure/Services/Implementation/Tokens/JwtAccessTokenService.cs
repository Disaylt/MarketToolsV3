using Identity.Application.Models;
using Identity.Application.Services;
using Identity.Domain.Seed;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using MarketToolsV3.ConfigurationManager.Models;
using Identity.Infrastructure.Services.Implementation.Claims;
using Identity.Infrastructure.Services.Abstract.Claims;
using Identity.Infrastructure.Services.Abstract.Tokens;

namespace Identity.Infrastructure.Services.Implementation.Tokens
{
    public class JwtAccessTokenService(IClaimsService<JwtAccessTokenDto> claimsService,
        IJwtTokenService jwtTokenService,
        IOptions<AuthConfig> authOptions)
        : ITokenService<JwtAccessTokenDto>
    {
        private readonly AuthConfig _authConfig = authOptions.Value;

        public string Create(JwtAccessTokenDto value)
        {
            DateTime expires = DateTime.UtcNow.AddMinutes(_authConfig.ExpireAccessTokenMinutes);
            IEnumerable<Claim> claims = claimsService.Create(value);
            SigningCredentials signingCredentials = jwtTokenService.CreateSigningCredentials(_authConfig.AuthSecret);

            JwtSecurityToken jwtSecurityToken = new(
                _authConfig.ValidIssuer,
                _authConfig.ValidAudience,
                claims,
                expires: expires,
                signingCredentials: signingCredentials);

            return new JwtSecurityTokenHandler()
                .WriteToken(jwtSecurityToken);
        }

        public async Task<bool> IsValid(string token)
        {
            TokenValidationResult result = await jwtTokenService
                .GetValidationResultAsync(token, _authConfig.AuthSecret,
                    checkValidateAudience: _authConfig.IsCheckValidAudience,
                    checkValidateIssuer: _authConfig.IsCheckValidIssuer);

            return result.IsValid;
        }

        public JwtAccessTokenDto Read(string token)
        {
            JwtSecurityToken jwtSecurityToken = jwtTokenService.ReadJwtToken(token);

            JwtAccessTokenDto jwtAccessTokenDto = new()
            {
                UserId = jwtSecurityToken.Claims.FindByType(ClaimTypes.NameIdentifier) ?? "",
                SessionId = jwtSecurityToken.Claims.FindByType(ClaimTypes.Sid) ?? ""
            };

            IEnumerable<string> roles = jwtSecurityToken.Claims
                .Where(x => x.Type == ClaimTypes.Role)
                .Select(x => x.Value);

            jwtAccessTokenDto.Roles.AddRange(roles);

            return jwtAccessTokenDto;
        }
    }
}
