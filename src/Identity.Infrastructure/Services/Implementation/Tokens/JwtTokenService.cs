using Identity.Domain.Seed;
using Identity.Infrastructure.Services.Abstract.Tokens;
using MarketToolsV3.ConfigurationManager.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Infrastructure.Services.Implementation.Tokens
{
    internal class JwtTokenService(IJwtSecurityTokenHandler jwtSecurityTokenHandler,
        IOptions<AuthConfig> authOptions,
        ILogger<JwtTokenService> logger)
        : IJwtTokenService
    {
        private readonly AuthConfig _authConfig = authOptions.Value;
        public virtual SigningCredentials CreateSigningCredentials(string secret)
        {
            byte[] secretBytes = Encoding.UTF8.GetBytes(secret);
            SymmetricSecurityKey authSigningKey = new(secretBytes);

            return new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256);
        }

        public virtual JwtSecurityToken ReadJwtToken(string token)
        {
            try
            {
                return jwtSecurityTokenHandler.ReadJwtToken(token);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unable to read jwt token - {token}", token);
                throw new RootServiceException(HttpStatusCode.BadRequest, "Не удалось прочить токен");
            }
        }

        public virtual async Task<TokenValidationResult> GetValidationResultAsync
        (string token,
            string secret,
            bool checkIssuerSigningKey = true,
            bool checkValidateIssuer = true,
            bool checkValidateAudience = true,
            bool checkValidateLifetime = true)
        {
            HMACSHA256 hmac = new(Encoding.UTF8.GetBytes(secret));

            TokenValidationParameters tokenValidationParameters = new()
            {
                ValidateIssuerSigningKey = checkIssuerSigningKey,
                IssuerSigningKey = new SymmetricSecurityKey(hmac.Key),
                ValidateIssuer = checkValidateIssuer,
                ValidateAudience = checkValidateAudience,
                ValidateLifetime = checkValidateLifetime,
                ValidAudience = _authConfig.ValidAudience,
                ValidIssuer = _authConfig.ValidIssuer,
                ClockSkew = TimeSpan.Zero,
            };
            return await jwtSecurityTokenHandler.ValidateTokenAsync(token, tokenValidationParameters);
        }
    }
}
