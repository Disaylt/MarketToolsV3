using Identity.Application.Models;
using Identity.Domain.Seed;
using Identity.Infrastructure.Services.Claims;
using Identity.Infrastructure.Services.Tokens;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        [TestCase("1")]
        [TestCase("2")]
        [TestCase("3")]
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

        [TestCase("1")]
        [TestCase("2")]
        [TestCase("3")]
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
        public async Task IsValid_CallValidationResultWithTrueParameters()
        {
            JwtAccessTokenService jwtAccessTokenService = new JwtAccessTokenService(_claimsServiceMock.Object,
                _jwtTokenServiceMock.Object,
                _optionsMock.Object);

            _optionsMock.SetupGet(x => x.Value.SecretAccessToken)
                .Returns(It.IsAny<string>());

            _jwtTokenServiceMock.Setup(x => x.GetValidationResultAsync(It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<bool>(),
                    It.IsAny<bool>(),
                    It.IsAny<bool>(),
                    It.IsAny<bool>()))
                .ReturnsAsync(new TokenValidationResult());

            await jwtAccessTokenService.IsValid(It.IsAny<string>());

            _jwtTokenServiceMock.Verify(x=> x.GetValidationResultAsync(It.IsAny<string>(),
                It.IsAny<string>(),
                It.Is<bool>(v=> v == true),
                It.Is<bool>(v => v == true),
                It.Is<bool>(v => v == true),
                It.Is<bool>(v => v == true)),
                Times.Once);
        }

        [TestCase("1")]
        [TestCase("2")]
        [TestCase("3")]
        public async Task IsValid_CallValidationResultWithSecretParameter(string secret)
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

            await jwtAccessTokenService.IsValid(It.IsAny<string>());

            _jwtTokenServiceMock.Verify(x => x.GetValidationResultAsync(It.IsAny<string>(),
                    It.Is<string>(v=> v == secret),
                    It.IsAny<bool>(),
                    It.IsAny<bool>(),
                    It.IsAny<bool>(),
                    It.IsAny<bool>()),
                Times.Once);
        }
    }
}
