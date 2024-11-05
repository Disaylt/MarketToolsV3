using Identity.Application.Models;
using Identity.Domain.Seed;
using Identity.Infrastructure.Services.Claims;
using Identity.Infrastructure.Services.Tokens;
using Microsoft.Extensions.Options;
using Moq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace MarketToolsV3.Users.UnitTests.Tests.Infrastructure.Services.Tokens
{
    internal class JwtAccessTokenServiceTests
    {
        private Mock<IClaimsService<JwtAccessTokenDto>> _claimsServiceMock;
        private Mock<IJwtTokenService> _jwtTokenServiceMock;
        private Mock<IOptions<ServiceConfiguration>> _optionsMock;

        [SetUp]
        public void Setup()
        {
            _claimsServiceMock = new Mock<IClaimsService<JwtAccessTokenDto>>();
            _jwtTokenServiceMock = new Mock<IJwtTokenService>();
            _optionsMock = new Mock<IOptions<ServiceConfiguration>>();
        }

        [TestCaseSource(nameof(CreateTestStrings))]
        public void Read_ReturnUserIdInToken(string userId)
        {
            JwtAccessTokenService jwtAccessTokenService = new JwtAccessTokenService(_claimsServiceMock.Object,
                _jwtTokenServiceMock.Object,
                _optionsMock.Object);

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                claims: new List<Claim> { new Claim(ClaimTypes.NameIdentifier, userId) });

            _jwtTokenServiceMock.Setup(x => x.ReadJwtToken(It.IsAny<string>()))
                .Returns(jwtSecurityToken);

            JwtAccessTokenDto jwtAccessToken = jwtAccessTokenService.Read(It.IsAny<string>());

            Assert.That(jwtAccessToken.UserId, Is.EqualTo(userId));
        }

        [TestCaseSource(nameof(CreateTestStrings))]
        public void Read_ContainsRole(string role)
        {
            JwtAccessTokenService jwtAccessTokenService = new JwtAccessTokenService(_claimsServiceMock.Object,
                _jwtTokenServiceMock.Object,
                _optionsMock.Object);

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                claims: new List<Claim> { new Claim(ClaimTypes.Role, role) });

            _jwtTokenServiceMock.Setup(x => x.ReadJwtToken(It.IsAny<string>()))
                .Returns(jwtSecurityToken);

            JwtAccessTokenDto jwtAccessToken = jwtAccessTokenService.Read(It.IsAny<string>());

            Assert.Contains(role, jwtAccessToken.Roles);
        }

        [Test]
        [TestCaseSource(nameof(CreateTokenAndSecret))]
        public async Task IsValid_CallValidationResultWithTestParameters(string token, string secret)
        {
            JwtAccessTokenService jwtAccessTokenService = new JwtAccessTokenService(_claimsServiceMock.Object,
                _jwtTokenServiceMock.Object,
                _optionsMock.Object);

            _optionsMock.SetupGet(x => x.Value.SecretAccessToken)
                .Returns(secret);

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
        private static IEnumerable<TestCaseData> CreateTokenAndSecret()
        {
            yield return new TestCaseData("token-1","secret-1");
            yield return new TestCaseData("token-2","secret-2");
            yield return new TestCaseData("token-3","secret-3");
            yield return new TestCaseData("token-4","secret-4");
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
