using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Identity.Application.Commands;
using Identity.Application.Models;
using Identity.Application.Services;
using Identity.Domain.Entities;
using Identity.Domain.Seed;
using Microsoft.Extensions.Logging;
using Moq;

namespace MarketToolsV3.Users.UnitTests.Tests.Applications.Commands
{
    internal class CreateAuthInfoHandlerTest
    {
        private Mock<IRepository<Session>> _sessionRepositoryMock;
        private ILogger<CreateAuthInfoHandler> _logger;
        private Mock<ITokenService<JwtAccessTokenDto>> _accessTokenServiceMock;
        private Mock<ITokenService<JwtRefreshTokenDto>> _refreshTokenServiceMock;
        private Mock<ISessionService> _sessionServiceMock;
        private CreateAuthInfo _command;

        [SetUp]
        public void Setup()
        {
            _logger = new Mock<ILogger<CreateAuthInfoHandler>>().Object;
            _sessionRepositoryMock = new Mock<IRepository<Session>>();
            _accessTokenServiceMock = new Mock<ITokenService<JwtAccessTokenDto>>();
            _refreshTokenServiceMock = new Mock<ITokenService<JwtRefreshTokenDto>>();
            _sessionServiceMock = new Mock<ISessionService>();
            _command = new CreateAuthInfo
            {
                Details = new AuthDetailsDto
                {
                    AuthToken = "access-token-1",
                    SessionToken = "refresh-token-1"
                }
            };
        }

        [Test]
        public async Task Handler_WhenAccessTokenValid_ReturnValid()
        {
            CreateAuthInfoHandler commandHandler = new(_sessionRepositoryMock.Object,
                _logger,
                _accessTokenServiceMock.Object,
                _refreshTokenServiceMock.Object,
                _sessionServiceMock.Object);

            _accessTokenServiceMock.Setup(x => x.IsValid(It.IsAny<string>()))
                .ReturnsAsync(true);

            AuthInfoDto result = await commandHandler.Handle(_command, It.IsAny<CancellationToken>());

            Assert.Multiple(() =>
            {
                Assert.That(result.IsValid, Is.True);
                Assert.That(result.Refreshed, Is.False);
            });
        }

        [Test]
        public async Task Handler_WhenRefreshTokenNotValid_ReturnNotValid()
        {
            CreateAuthInfoHandler commandHandler = new(_sessionRepositoryMock.Object,
                _logger,
                _accessTokenServiceMock.Object,
                _refreshTokenServiceMock.Object,
                _sessionServiceMock.Object);

            _accessTokenServiceMock.Setup(x => x.IsValid(It.IsAny<string>()))
                .ReturnsAsync(false);
            _refreshTokenServiceMock.Setup(x=> x.IsValid(It.IsAny<string>()))
                .ReturnsAsync(false);

            AuthInfoDto result = await commandHandler.Handle(_command, It.IsAny<CancellationToken>());

            Assert.Multiple(() =>
            {
                Assert.That(result.IsValid, Is.False);
                Assert.That(result.Refreshed, Is.False);
            });
        }

        [Test]
        public async Task Handler_WhenSessionNotActive_ReturnNotValid()
        {
            CreateAuthInfoHandler commandHandler = new(_sessionRepositoryMock.Object,
                _logger,
                _accessTokenServiceMock.Object,
                _refreshTokenServiceMock.Object,
                _sessionServiceMock.Object);

            _accessTokenServiceMock.Setup(x => x.IsValid(It.IsAny<string>()))
                .ReturnsAsync(false);
            _refreshTokenServiceMock.Setup(x => x.IsValid(It.IsAny<string>()))
                .ReturnsAsync(true);
            _refreshTokenServiceMock.Setup(x => x.Read(It.IsAny<string>()))
                .Returns(new JwtRefreshTokenDto { Id = "" });
            _sessionRepositoryMock
                .Setup(x => x.FindByIdRequiredAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Session(It.IsAny<string>(), It.IsAny<string>()) { IsActive = false });

            AuthInfoDto result = await commandHandler.Handle(_command, It.IsAny<CancellationToken>());

            Assert.Multiple(() =>
            {
                Assert.That(result.IsValid, Is.False);
                Assert.That(result.Refreshed, Is.False);
            });
        }

        [Test]
        public async Task Handler_WhenRefreshTokenNotEqualsSessionRefreshToken_ReturnNotValid()
        {
            CreateAuthInfoHandler commandHandler = new(_sessionRepositoryMock.Object,
                _logger,
                _accessTokenServiceMock.Object,
                _refreshTokenServiceMock.Object,
                _sessionServiceMock.Object);

            _accessTokenServiceMock.Setup(x => x.IsValid(It.IsAny<string>()))
                .ReturnsAsync(false);
            _refreshTokenServiceMock.Setup(x => x.IsValid(It.IsAny<string>()))
                .ReturnsAsync(true);
            _refreshTokenServiceMock.Setup(x => x.Read(It.IsAny<string>()))
                .Returns(new JwtRefreshTokenDto { Id = "" });
            _sessionRepositoryMock
                .Setup(x => x.FindByIdRequiredAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Session(It.IsAny<string>(), It.IsAny<string>()) { IsActive = true, Token = "refresh-token-2" });

            AuthInfoDto result = await commandHandler.Handle(_command, It.IsAny<CancellationToken>());

            Assert.Multiple(() =>
            {
                Assert.That(result.IsValid, Is.False);
                Assert.That(result.Refreshed, Is.False);
            });
        }

        [Test]
        public async Task Handler_ReturnNewAuthInfo()
        {
            CreateAuthInfoHandler commandHandler = new(_sessionRepositoryMock.Object,
                _logger,
                _accessTokenServiceMock.Object,
                _refreshTokenServiceMock.Object,
                _sessionServiceMock.Object);

            _accessTokenServiceMock.Setup(x => x.IsValid(It.IsAny<string>()))
                .ReturnsAsync(false);
            _refreshTokenServiceMock.Setup(x => x.IsValid(It.IsAny<string>()))
                .ReturnsAsync(true);
            _refreshTokenServiceMock.Setup(x => x.Read(It.IsAny<string>()))
                .Returns(new JwtRefreshTokenDto { Id = "" });
            _sessionRepositoryMock
                .Setup(x => x.FindByIdRequiredAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Session(It.IsAny<string>(), It.IsAny<string>()) { IsActive = true, Token = "refresh-token-1" });
            _refreshTokenServiceMock.Setup(x => x.Create(It.IsAny<JwtRefreshTokenDto>()))
                .Returns("refresh-token-2");
            _accessTokenServiceMock.Setup(x => x.Create(It.IsAny<JwtAccessTokenDto>()))
                .Returns("access-token-2");

            AuthInfoDto result = await commandHandler.Handle(_command, It.IsAny<CancellationToken>());

            Assert.Multiple(() =>
            {
                Assert.That(result.IsValid, Is.True);
                Assert.That(result.Refreshed, Is.True);
                Assert.That(result.Details?.AuthToken, Is.EqualTo("access-token-2"));
                Assert.That(result.Details?.SessionToken, Is.EqualTo("refresh-token-2"));
            });
        }
    }
}
