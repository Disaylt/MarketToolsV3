using Identity.Application.Models;
using Identity.Domain.Seed;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using MarketToolsV3.ConfigurationManager.Models;
using Identity.Infrastructure.Services.Implementation.Claims;
using Identity.Infrastructure.Services.Abstract.Claims;
using Identity.Infrastructure.Services.Abstract.Tokens;
using Identity.Application.Services.Abstract;

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

            string? moduleType = jwtSecurityToken.Claims.FindByType("moduleType");
            string? modulePath = jwtSecurityToken.Claims.FindByType("modulePath");

            if (int.TryParse(jwtSecurityToken.Claims.FindByType("moduleId"), out var moduleId)
                && moduleType != null
                && modulePath != null)
            {
                jwtAccessTokenDto.ServiceAuthInfo = new()
                {
                    Type = moduleType,
                    Id = moduleId,
                    Path = modulePath,
                    Roles = ParseModuleRoles(jwtSecurityToken.Claims),
                    ClaimTypeAndValuePairs = ParseModuleClaims(jwtSecurityToken.Claims)
                };
            }

            IEnumerable<string> roles = ParseRoles(jwtSecurityToken.Claims);
            jwtAccessTokenDto.Roles.AddRange(roles);

            return jwtAccessTokenDto;
        }

        private IReadOnlyCollection<string> ParseRoles(IEnumerable<Claim> claims)
        {
            return claims
                .Where(x => x.Type == ClaimTypes.Role)
                .Select(x => x.Value)
                .ToList();
        }

        private static IReadOnlyCollection<string> ParseModuleRoles(IEnumerable<Claim> claims)
        {
            string moduleRoleType = $"module_{ClaimTypes.Role}";

            return claims
                .Where(x => x.Type == moduleRoleType)
                .Select(x => x.Value)
                .ToList();
        }

        private static IReadOnlyCollection<ModuleClaimDto> ParseModuleClaims(IEnumerable<Claim> claims)
        {
            List<ModuleClaimDto> result = [];
            var tempStrValues = claims
                .Where(x => x.Type.StartsWith("mp_"))
                .Select(x => new { SplitType = x.Type.Split('_'), x.Value });

            foreach (var claim in tempStrValues)
            {
                if (claim.SplitType.Length > 1
                    && int.TryParse(claim.Value, out var valueResult))
                {
                    result.Add(new ModuleClaimDto
                    {
                        Type = claim.SplitType[1],
                        Value = valueResult
                    });
                }
            }

            return result;
        }
    }
}
