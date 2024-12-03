using Identity.Application.Models;
using Identity.Domain.Seed;
using Identity.Infrastructure.Services.Claims;
using Identity.Infrastructure.Services.Tokens;
using MarketToolsV3.ConfigurationManager.Models;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Moq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MarketToolsV3.Users.UnitTests.Tests.Infrastructure.Services.Tokens
{
    internal class JwtAccessTokenServiceTests
    {
        private Mock<IClaimsService<JwtAccessTokenDto>> _claimsServiceMock;
        private Mock<IJwtTokenService> _jwtTokenServiceMock;
        private Mock<IOptions<AuthConfig>> _authConfigOptionsMock;

        [SetUp]
        public void Setup()
        {
            _claimsServiceMock = new Mock<IClaimsService<JwtAccessTokenDto>>();
            _jwtTokenServiceMock = new Mock<IJwtTokenService>();
            _authConfigOptionsMock = new Mock<IOptions<AuthConfig>>();
        }

        [TestCaseSource(nameof(CreateTestStrings))]
        public void Read_ReturnUserIdInToken(string userId)
        {
            JwtAccessTokenService jwtAccessTokenService = new(_claimsServiceMock.Object,
                _jwtTokenServiceMock.Object,
                _authConfigOptionsMock.Object);

            JwtSecurityToken jwtSecurityToken = new(
                claims: [new(ClaimTypes.NameIdentifier, userId)]);

            _jwtTokenServiceMock.Setup(x => x.ReadJwtToken(It.IsAny<string>()))
                .Returns(jwtSecurityToken);

            JwtAccessTokenDto jwtAccessToken = jwtAccessTokenService.Read(It.IsAny<string>());

            Assert.That(jwtAccessToken.UserId, Is.EqualTo(userId));
        }

        [TestCaseSource(nameof(CreateTestStrings))]
        public void Read_ContainsRole(string role)
        {
            JwtAccessTokenService jwtAccessTokenService = new(_claimsServiceMock.Object,
                _jwtTokenServiceMock.Object,
                _authConfigOptionsMock.Object);

            JwtSecurityToken jwtSecurityToken = new(
                claims: [new(ClaimTypes.Role, role)]);

            _jwtTokenServiceMock.Setup(x => x.ReadJwtToken(It.IsAny<string>()))
                .Returns(jwtSecurityToken);

            JwtAccessTokenDto jwtAccessToken = jwtAccessTokenService.Read(It.IsAny<string>());

            Assert.That(jwtAccessToken.Roles, Does.Contain(role));
        }

        [TestCaseSource(nameof(CreateTokenAndSecret))]
        public async Task IsValid_CallValidationResultWithTestParameters(string token, string secret)
        {
            _authConfigOptionsMock.SetupGet(x => x.Value)
                .Returns(new AuthConfig
                {
                    AuthSecret = secret
                });

            JwtAccessTokenService jwtAccessTokenService = new(_claimsServiceMock.Object,
                _jwtTokenServiceMock.Object,
                _authConfigOptionsMock.Object);


            _jwtTokenServiceMock.Setup(x => x.GetValidationResultAsync(It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<bool>(),
                    It.IsAny<bool>(),
                    It.IsAny<bool>(),
                    It.IsAny<bool>()))
                .ReturnsAsync(new TokenValidationResult());

            await jwtAccessTokenService.IsValid(token);

            _jwtTokenServiceMock.Verify(x=> x.GetValidationResultAsync(It.Is<string>(v=> v == token),
                It.Is<string>(v=> v == secret),
                It.Is<bool>(v=> v == true),
                It.Is<bool>(v => v == true),
                It.Is<bool>(v => v == true),
                It.Is<bool>(v => v == true)),
                Times.Once);
        }

        [TestCaseSource(nameof(CreateTestStrings))]
        public void Create_UseSecretAccessToken(string secret)
        {
            _authConfigOptionsMock.SetupGet(x => x.Value)
                .Returns(new AuthConfig
                {
                    AuthSecret = secret,
                    ExpireAccessTokenMinutes = It.IsAny<int>(),
                    ValidIssuer = It.IsAny<string>(),
                    ValidAudience = It.IsAny<string>(),
                });

            JwtAccessTokenService jwtAccessTokenService = new(_claimsServiceMock.Object,
                _jwtTokenServiceMock.Object,
                _authConfigOptionsMock.Object);


            jwtAccessTokenService.Create(It.IsAny<JwtAccessTokenDto>());

            _jwtTokenServiceMock.Verify(x=> 
                    x.CreateSigningCredentials(It.Is<string>(v=> v == secret)),
                Times.Once);
        }

        private static IEnumerable<TestCaseData> CreateTokenAndSecret()
        {
            yield return new TestCaseData("token-1","secret-1");
            yield return new TestCaseData("token-2","secret-2");
            yield return new TestCaseData("token-3","secret-3");
            yield return new TestCaseData("token-4","secret-4");
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
