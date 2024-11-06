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
        private Mock<ILogger<CreateAuthInfoHandler>> _loggerMock;
        private Mock<ITokenService<JwtAccessTokenDto>> _accessTokenServiceMock;
        private Mock<ITokenService<JwtRefreshTokenDto>> _refreshTokenServiceMock;
        private Mock<ISessionService> _sessionServiceMock;

        [SetUp]
        public void Setup()
        {
            _sessionRepositoryMock = new Mock<IRepository<Session>>();
            _loggerMock = new Mock<ILogger<CreateAuthInfoHandler>>();
            _accessTokenServiceMock = new Mock<ITokenService<JwtAccessTokenDto>>();
            _refreshTokenServiceMock = new Mock<ITokenService<JwtRefreshTokenDto>>();
            _sessionServiceMock = new Mock<ISessionService>();
        }

        [Test]
        public async Task Handler_ReturnAccessTokenNotValid()
        {
            CreateAuthInfo command = new CreateAuthInfo
            {
                Details = new AuthDetailsDto
                {
                    AuthToken = "",
                    SessionToken = ""
                }
            };


        }
    }
}
