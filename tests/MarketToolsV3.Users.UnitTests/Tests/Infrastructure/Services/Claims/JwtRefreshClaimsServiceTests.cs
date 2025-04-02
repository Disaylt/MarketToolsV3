using Identity.Application.Models;
using Identity.Infrastructure.Services.Implementation.Claims;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MarketToolsV3.Users.UnitTests.Tests.Infrastructure.Services.Claims
{
    internal class JwtRefreshClaimsServiceTests
    {
        [Test]
        public void Create_ContainsJti()
        {
            JwtRefreshClaimsService service = new();

            JwtRefreshTokenDto refreshToken = new()
            {
                Id = "",
                AccessTokenId = ""
            };

            IEnumerable<string> claimTypes = service
                .Create(refreshToken)
                .Select(x=> x.Type)
                .ToList();

            Assert.That(claimTypes, Does.Contain(JwtRegisteredClaimNames.Jti));
        }

        [Test]
        public void Create_ContainsIat()
        {
            JwtRefreshClaimsService service = new();

            JwtRefreshTokenDto refreshToken = new()
            {
                Id = "",
                AccessTokenId = ""
            };

            IEnumerable<string> claimTypes = service
                .Create(refreshToken)
                .Select(x => x.Type)
                .ToList();

            Assert.That(claimTypes, Does.Contain(JwtRegisteredClaimNames.Iat));
        }

        [Test]
        public void Create_ContainsSid()
        {
            JwtRefreshClaimsService service = new();

            JwtRefreshTokenDto refreshToken = new()
            {
                Id = "",
                AccessTokenId = ""
            };

            IEnumerable<string> claimTypes = service
                .Create(refreshToken)
                .Select(x => x.Type)
                .ToList();

            Assert.That(claimTypes, Does.Contain(ClaimTypes.Sid));
        }
    }
}
