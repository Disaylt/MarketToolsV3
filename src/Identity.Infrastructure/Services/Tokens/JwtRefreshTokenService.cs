using Identity.Application.Models;
using Identity.Application.Services;
using Identity.Domain.Seed;
using Identity.Infrastructure.Services.Claims;
using MarketToolsV3.ConfigurationManager.Models;
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
        IOptions<ServiceConfiguration> serviceConfigurationOptions,
        IOptions<AuthConfig> authOptions)
        : ITokenService<JwtRefreshTokenDto>
    {
        private readonly ServiceConfiguration _serviceConfiguration = serviceConfigurationOptions.Value;
        private readonly AuthConfig _authConfig = authOptions.Value;

        public string Create(JwtRefreshTokenDto value)
        {
            DateTime expires = DateTime.UtcNow.AddHours(serviceConfigurationOptions.Value.ExpireRefreshTokenHours);
            IEnumerable<Claim> claims = claimsService.Create(value);
            SigningCredentials signingCredentials = jwtTokenService.CreateSigningCredentials(serviceConfigurationOptions.Value.SecretRefreshToken);

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
                .GetValidationResultAsync(token,
                    _serviceConfiguration.SecretRefreshToken);

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
