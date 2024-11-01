using Identity.Application.Models;
using Identity.Application.Services;
using Identity.Domain.Seed;
using Identity.Infrastructure.Services.Claims;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Infrastructure.Services.Tokens
{
    internal class JwtAccessTokenService(IClaimsService<JwtAccessTokenDto> claimsService,
        IJwtTokenService jwtTokenService,
        IOptions<ServiceConfiguration> options)
        : ITokenService<JwtAccessTokenDto>
    {
        private readonly ServiceConfiguration _configuration = options.Value;
        public string Create(JwtAccessTokenDto value)
        {
            DateTime expires = DateTime.UtcNow.AddMinutes(_configuration.ExpireAccessTokenMinutes);
            IEnumerable<Claim> claims = claimsService.Create(value);
            SigningCredentials signingCredentials = jwtTokenService.CreateSigningCredentials(_configuration.SecretAccessToken);

            JwtSecurityToken jwtSecurityToken = new(
                _configuration.ValidIssuer,
                _configuration.ValidAudience,
                claims,
                expires: expires,
                signingCredentials: signingCredentials);

            return new JwtSecurityTokenHandler()
                .WriteToken(jwtSecurityToken);
        }

        public async Task<bool> IsValid(string token)
        {
            TokenValidationResult result = await jwtTokenService.GetValidationResultAsync(token, _configuration.SecretAccessToken);

            return result.IsValid;
        }

        public JwtAccessTokenDto Read(string token)
        {
            JwtSecurityToken jwtSecurityToken = jwtTokenService.ReadJwtToken(token);

            JwtAccessTokenDto jwtAccessTokenDto = new JwtAccessTokenDto
            {
                UserId = jwtSecurityToken.Claims.FindByType(ClaimTypes.NameIdentifier) ?? ""
            };

            IEnumerable<string> roles = jwtSecurityToken.Claims
                .Where(x => x.Type == ClaimTypes.Role)
                .Select(x => x.Value);

            jwtAccessTokenDto.Roles.AddRange(roles);

            return jwtAccessTokenDto;
        }
    }
}
