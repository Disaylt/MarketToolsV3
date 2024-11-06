using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Castle.Core.Logging;
using Identity.Application.Commands;
using Identity.Application.Models;
using Identity.Application.Services;
using Identity.Domain.Entities;
using Identity.Domain.Seed;
using MarketToolsV3.Users.UnitTests.Source;
using Microsoft.Extensions.Logging;
using Moq;
using ILogger = Castle.Core.Logging.ILogger;

namespace MarketToolsV3.Users.UnitTests.Tests.Applications.Commands
{
    internal class LoginCommandHandlerTests
    {
        private Mock<IIdentityPersonService> _identityPersonServiceMock;
        private Mock<ISessionService> _sessionServiceMock;
        private Mock<ITokenService<JwtAccessTokenDto>> _accessTokenServiceMock;
        private Mock<ITokenService<JwtRefreshTokenDto>> _refreshTokenServiceMock;
        private ILogger<LoginCommandHandler> _logger;
        private LoginCommand _command;

        [SetUp]
        public void Setup()
        {
            _identityPersonServiceMock = new Mock<IIdentityPersonService>();
            _sessionServiceMock = new Mock<ISessionService>();
            _accessTokenServiceMock = new Mock<ITokenService<JwtAccessTokenDto>>();
            _refreshTokenServiceMock = new Mock<ITokenService<JwtRefreshTokenDto>>();
            _logger = new Mock<ILogger<LoginCommandHandler>>().Object;
            _command = new LoginCommand
            {
                Email = "email-1",
                Password = "password-1",
                UserAgent = "user-agent-1"
            };
        }

        [Test]
        public void Handle_WhenUserNull_ReturnException()
        {
            LoginCommandHandler commandHandler = new(
                _identityPersonServiceMock.Object,
                _sessionServiceMock.Object,
                _logger,
                _accessTokenServiceMock.Object,
                _refreshTokenServiceMock.Object);

            _identityPersonServiceMock
                .Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(It.IsAny<IdentityPerson>());

            RootServiceException ex = Assert.CatchAsync<RootServiceException>(() =>
                commandHandler.Handle(_command, It.IsAny<CancellationToken>()));

            Assert.That(ex.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));

        }

        [Test]
        public void Handle_WhenBadCheckPassword_ReturnException()
        {
            LoginCommandHandler commandHandler = new(
                _identityPersonServiceMock.Object,
                _sessionServiceMock.Object,
                _logger,
                _accessTokenServiceMock.Object,
                _refreshTokenServiceMock.Object);

            _identityPersonServiceMock
                .Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(new IdentityPerson());

            _identityPersonServiceMock
                .Setup(x=> x.CheckPassword(It.IsAny<IdentityPerson>(), It.IsAny<string>()))
                .ReturnsAsync(false);

            RootServiceException ex = Assert.CatchAsync<RootServiceException>(() =>
                commandHandler.Handle(_command, It.IsAny<CancellationToken>()));

            Assert.That(ex.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));

        }

        [Test]
        public async Task Handle_ReturnAuthDetails()
        {
            string accessTokenValue = "access-token-1";
            string refreshTokenValue = "refresh-token-1";

            LoginCommandHandler commandHandler = new(
                _identityPersonServiceMock.Object,
                _sessionServiceMock.Object,
                _logger,
                _accessTokenServiceMock.Object,
                _refreshTokenServiceMock.Object);

            _identityPersonServiceMock
                .Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(new IdentityPerson());

            _identityPersonServiceMock
                .Setup(x => x.CheckPassword(It.IsAny<IdentityPerson>(), It.IsAny<string>()))
                .ReturnsAsync(true);

            _accessTokenServiceMock.Setup(x => x.Create(It.IsAny<JwtAccessTokenDto>()))
                .Returns(accessTokenValue);
            _refreshTokenServiceMock.Setup(x => x.Create(It.IsAny<JwtRefreshTokenDto>()))
                .Returns(refreshTokenValue);

            AuthDetailsDto result = await commandHandler.Handle(_command, It.IsAny<CancellationToken>());

            Assert.Multiple(() =>
            {
                Assert.That(result.AuthToken, Is.EqualTo(accessTokenValue));
                Assert.That(result.SessionToken, Is.EqualTo(refreshTokenValue));
            });
        }
    }
}
