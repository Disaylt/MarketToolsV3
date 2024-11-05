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
    public class JwtRefreshTokenService(IClaimsService<JwtRefreshTokenDto> claimsService,
        IJwtTokenService jwtTokenService,
        IOptions<ServiceConfiguration> options)
        : ITokenService<JwtRefreshTokenDto>
    {
        private readonly ServiceConfiguration _configuration = options.Value;

        public string Create(JwtRefreshTokenDto value)
        {
            DateTime expires = DateTime.UtcNow.AddMinutes(_configuration.ExpireRefreshTokenHours);
            IEnumerable<Claim> claims = claimsService.Create(value);
            SigningCredentials signingCredentials = jwtTokenService.CreateSigningCredentials(_configuration.SecretRefreshToken);

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
            TokenValidationResult result = await jwtTokenService
                .GetValidationResultAsync(token,
                    _configuration.SecretRefreshToken);

            return result.IsValid;
        }

        public JwtRefreshTokenDto Read(string token)
        {
            JwtSecurityToken jwtSecurityToken = jwtTokenService.ReadJwtToken(token);

            return new JwtRefreshTokenDto
            {
                Id = jwtSecurityToken.Claims.FindByType(ClaimTypes.Sid) ?? ""
            };
        }
    }
}
