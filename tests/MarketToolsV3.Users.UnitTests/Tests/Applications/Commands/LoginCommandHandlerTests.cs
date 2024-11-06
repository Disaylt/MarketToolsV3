using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Core.Logging;
using Identity.Application.Commands;
using Identity.Application.Models;
using Identity.Application.Services;
using MarketToolsV3.Users.UnitTests.Source;
using Microsoft.Extensions.Logging;
using Moq;

namespace MarketToolsV3.Users.UnitTests.Tests.Applications.Commands
{
    internal class LoginCommandHandlerTests
    {
        private Mock<IIdentityPersonService> _identityPersonServiceMock;
        private Mock<ISessionService> _sessionServiceMock;
        private Mock<ITokenService<JwtAccessTokenDto>> _accessTokenServiceMock;
        private Mock<ITokenService<JwtRefreshTokenDto>> _refreshTokenServiceMock;
        private ILogger<LoginCommandHandler> _logger;

        [SetUp]
        public void Setup()
        {
            _identityPersonServiceMock = new Mock<IIdentityPersonService>();
            _sessionServiceMock = new Mock<ISessionService>();
            _accessTokenServiceMock = new Mock<ITokenService<JwtAccessTokenDto>>();
            _refreshTokenServiceMock = new Mock<ITokenService<JwtRefreshTokenDto>>();
            _logger = TestLogging.Create<LoginCommandHandler>();
        }
    }
}
