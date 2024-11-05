using Identity.Application.Models;
using Identity.Domain.Seed;
using Identity.Infrastructure.Services.Claims;
using Identity.Infrastructure.Services.Tokens;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace MarketToolsV3.Users.UnitTests.Tests.Infrastructure.Services.Tokens
{
    internal class JwtRefreshTokenServiceTests
    {
        private Mock<IClaimsService<JwtRefreshTokenDto>> _claimsServiceMock;
        private Mock<IJwtTokenService> _jwtTokenServiceMock;
        private Mock<IOptions<ServiceConfiguration>> _optionsMock;

        [SetUp]
        public void Setup()
        {
            _claimsServiceMock = new Mock<IClaimsService<JwtRefreshTokenDto>>();
            _jwtTokenServiceMock = new Mock<IJwtTokenService>();
            _optionsMock = new Mock<IOptions<ServiceConfiguration>>();
        }

        [TestCaseSource(nameof(CreateTestStrings))]
        public void Read_ReturnSessionIdInToken(string sessionId)
        {
            JwtRefreshTokenService jwtAccessTokenService = new JwtRefreshTokenService(_claimsServiceMock.Object,
                _jwtTokenServiceMock.Object,
                _optionsMock.Object);

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                claims: new List<Claim> { new(ClaimTypes.Sid, sessionId) });

            _jwtTokenServiceMock.Setup(x => x.ReadJwtToken(It.IsAny<string>()))
                .Returns(jwtSecurityToken);

            JwtRefreshTokenDto jwtRefreshToken = jwtAccessTokenService.Read(It.IsAny<string>());

            Assert.That(jwtRefreshToken.Id, Is.EqualTo(sessionId));
        }

        [TestCaseSource(nameof(CreateTokenAndSecret))]
        public async Task IsValid_CallValidationResultWithTestParameters(string token, string secret)
        {
            JwtRefreshTokenService jwtAccessTokenService = new JwtRefreshTokenService(_claimsServiceMock.Object,
                _jwtTokenServiceMock.Object,
                _optionsMock.Object);

            _optionsMock.SetupGet(x => x.Value.SecretRefreshToken)
                .Returns(secret);

            _jwtTokenServiceMock.Setup(x => x.GetValidationResultAsync(It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<bool>(),
                    It.IsAny<bool>(),
                    It.IsAny<bool>(),
                    It.IsAny<bool>()))
                .ReturnsAsync(new TokenValidationResult());

            await jwtAccessTokenService.IsValid(token);

            _jwtTokenServiceMock.Verify(x => x.GetValidationResultAsync(It.Is<string>(v => v == token),
                    It.Is<string>(v => v == secret),
                    It.Is<bool>(v => v == true),
                    It.Is<bool>(v => v == true),
                    It.Is<bool>(v => v == true),
                    It.Is<bool>(v => v == true)),
                Times.Once);
        }

        private static IEnumerable<TestCaseData> CreateTokenAndSecret()
        {
            yield return new TestCaseData("token-1", "secret-1");
            yield return new TestCaseData("token-2", "secret-2");
            yield return new TestCaseData("token-3", "secret-3");
            yield return new TestCaseData("token-4", "secret-4");
        }

        private static IEnumerable<TestCaseData> CreateTestNum()
        {
            yield return new TestCaseData(1);
            yield return new TestCaseData(2);
            yield return new TestCaseData(3);
            yield return new TestCaseData(4);
        }

        private static IEnumerable<TestCaseData> CreateTestStrings()
        {
            yield return new TestCaseData("yield-1");
            yield return new TestCaseData("yield-2");
            yield return new TestCaseData("yield-3");
            yield return new TestCaseData("yield-4");
            yield return new TestCaseData("yield-5");
        }
    }
}
