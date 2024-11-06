using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Identity.Application.Models;
using Identity.Application.Queries;
using Identity.Application.Services;
using Identity.Domain.Entities;
using Identity.Domain.Seed;
using Microsoft.Extensions.Options;
using Moq;

namespace MarketToolsV3.Users.UnitTests.Tests.Applications.Queries
{
    internal class CheckSessionActiveStatusQueryHandlerTests
    {
        private Mock<ITokenService<JwtRefreshTokenDto>> _tokenServiceMock;
        private Mock<ICacheRepository<SessionStatusDto>> _sessionCacheRepositoryMock;
        private Mock<IRepository<Session>> _sessionRepositoryMock;
        private Mock<IOptions<ServiceConfiguration>> _serviceConfigurationMock;
        private CheckSessionActiveStatusQuery _query;


        [SetUp]
        public void Setup()
        {
            _tokenServiceMock = new Mock<ITokenService<JwtRefreshTokenDto>>();
            _sessionRepositoryMock = new Mock<IRepository<Session>>();
            _sessionCacheRepositoryMock = new Mock<ICacheRepository<SessionStatusDto>>();
            _serviceConfigurationMock = new Mock<IOptions<ServiceConfiguration>>();
            _query = new CheckSessionActiveStatusQuery
            {
                RefreshToken = "refresh-token-1"
            };
        }

        [Test]
        public async Task Handle_WhenTokenNotValid_ReturnFalse()
        {
            CheckSessionActiveStatusQueryHandler queryHandler = new(
                _tokenServiceMock.Object,
                _sessionCacheRepositoryMock.Object,
                _sessionRepositoryMock.Object,
                _serviceConfigurationMock.Object);

            _tokenServiceMock.Setup(x => x.IsValid(It.IsAny<string>()))
                .ReturnsAsync(false);

            bool result = await queryHandler.Handle(_query, It.IsAny<CancellationToken>());

            Assert.That(result, Is.False);
        }

        [TestCase(true)]
        [TestCase(false)]
        public async Task Handle_WhenSessionGetFromCache_ReturnSessionResult(bool expectResult)
        {
            CheckSessionActiveStatusQueryHandler queryHandler = new(
                _tokenServiceMock.Object,
                _sessionCacheRepositoryMock.Object,
                _sessionRepositoryMock.Object,
                _serviceConfigurationMock.Object);

            _tokenServiceMock.Setup(x => x.IsValid(It.IsAny<string>()))
                .ReturnsAsync(true);
            _tokenServiceMock.Setup(x => x.Read(It.IsAny<string>()))
                .Returns(new JwtRefreshTokenDto { Id = "" });
            _sessionCacheRepositoryMock.Setup(x => x.GetAsync(It.IsAny<string>()))
                .ReturnsAsync(new SessionStatusDto() { Id = "", IsActive = expectResult });

            bool result = await queryHandler.Handle(_query, It.IsAny<CancellationToken>());

            Assert.That(result, Is.EqualTo(expectResult));
            _sessionRepositoryMock.Verify(x=> 
                x.FindByIdRequiredAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()),
                Times.Never);
        }

        [TestCase(true)]
        [TestCase(false)]
        public async Task Handle_WhenCacheSessionNull_ReturnSessionResult(bool expectResult)
        {
            CheckSessionActiveStatusQueryHandler queryHandler = new(
                _tokenServiceMock.Object,
                _sessionCacheRepositoryMock.Object,
                _sessionRepositoryMock.Object,
                _serviceConfigurationMock.Object);

            _tokenServiceMock.Setup(x => x.IsValid(It.IsAny<string>()))
                .ReturnsAsync(true);
            _tokenServiceMock.Setup(x => x.Read(It.IsAny<string>()))
                .Returns(new JwtRefreshTokenDto { Id = "" });
            _sessionCacheRepositoryMock.Setup(x => x.GetAsync(It.IsAny<string>()))
                .ReturnsAsync(It.IsAny<SessionStatusDto>());
            _sessionRepositoryMock
                .Setup(x => x.FindByIdRequiredAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Session("", "") { IsActive = expectResult });
            _serviceConfigurationMock.SetupGet(x => x.Value.ExpireRefreshTokenHours)
                .Returns(2);

            bool result = await queryHandler.Handle(_query, It.IsAny<CancellationToken>());

            Assert.That(result, Is.EqualTo(expectResult));
            _sessionRepositoryMock.Verify(x =>
                    x.FindByIdRequiredAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()),
                Times.Once);
        }
    }
}
