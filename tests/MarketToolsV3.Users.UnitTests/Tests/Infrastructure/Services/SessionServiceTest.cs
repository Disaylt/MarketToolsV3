using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Identity.Application.Services;
using Identity.Domain.Entities;
using Identity.Domain.Seed;
using Identity.Infrastructure.Database;
using Identity.Infrastructure.Services;
using MarketToolsV3.Users.UnitTests.Mock;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Moq;

namespace MarketToolsV3.Users.UnitTests.Tests.Infrastructure.Services
{
    internal class SessionServiceTest
    {
        private Mock<IRepository<Session>> _sessionRepositoryMock;
        private Mock<IOptions<ServiceConfiguration>> _serviceConfigurationMock;
        private Mock<IEventRepository> _eventRepositoryMock;

        [SetUp]
        public void Setup()
        {
            _sessionRepositoryMock = new();
            _eventRepositoryMock = new();
            _serviceConfigurationMock = new();
        }

        [TestCase("token-2", "user-agent-2")]
        [TestCase("token-1", "user-agent-1")]
        public async Task UpdateAsync_ExpectedNewValues(string token, string userAgent)
        {
            Session session = new Session(It.IsAny<string>(), It.IsAny<string>());

            _sessionRepositoryMock.Setup(x => x.UnitOfWork.SaveChangesAsync(default));

            await using IdentityDbContext context = MemoryDbContext.Create(Guid.NewGuid().ToString());

            SessionService sessionService = new SessionService(
                _sessionRepositoryMock.Object,
                context,
                _serviceConfigurationMock.Object,
                _eventRepositoryMock.Object);

            await sessionService.UpdateAsync(session, token, userAgent);

            Assert.That(userAgent, Is.EqualTo(session.UserAgent));
            Assert.That(token, Is.EqualTo(session.Token));

            _sessionRepositoryMock.Verify(x => x.UnitOfWork.SaveEntitiesAsync(default), Times.Once());
        }

        [TestCase("0", 1, 0)]
        [TestCase("1", 1, 1)]
        [TestCase("1", 3, 2)]
        [TestCase("2", 3, 3)]
        [TestCase("2", 1, 2)]
        public async Task GetActiveSessionsAsync_ExpectedQuantity(string identityId, int expireRefreshToken, int expectedResult)
        {
            await using IdentityDbContext context = MemoryDbContext.Create(Guid.NewGuid().ToString());
            await FillSessionFor_GetActiveSessionsAsync_ExpectedQuantity(context);

            _serviceConfigurationMock.SetupGet(x => x.Value.ExpireRefreshTokenHours).Returns(expireRefreshToken);

            SessionService sessionService = new SessionService(
                _sessionRepositoryMock.Object,
                context,
                _serviceConfigurationMock.Object,
                _eventRepositoryMock.Object);

            IEnumerable<Session> sessions = await sessionService.GetActiveSessionsAsync(identityId);

            Assert.That(sessions.Count(), Is.EqualTo(expectedResult));
        }

        [Test]
        public async Task AddAsync_Expected1Quantity()
        {
            await using IdentityDbContext context = MemoryDbContext.Create(Guid.NewGuid().ToString());

            _sessionRepositoryMock.Setup(x => x.UnitOfWork.SaveChangesAsync(CancellationToken.None))
                .Returns(context.SaveChangesAsync());

            SessionService sessionService = new SessionService(
                _sessionRepositoryMock.Object,
                context,
                _serviceConfigurationMock.Object,
                _eventRepositoryMock.Object);

            await sessionService.AddAsync(new Session("", ""));

            Assert.That(context.Sessions.Count(), Is.EqualTo(1));
        }

        [Test]
        public async Task DeleteAsync_Expected0Quantity()
        {
            await using IdentityDbContext context = MemoryDbContext.Create(Guid.NewGuid().ToString());

            await FillSessionFor_DeleteAsync(context);

            _sessionRepositoryMock.Setup(x => x.DeleteAsync(It.IsAny<Session>(), It.IsAny<CancellationToken>()))
                .Returns(context.Sessions.AddAsync(It.IsAny<Session>(), It.IsAny<CancellationToken>()));
            _sessionRepositoryMock.Setup(x => x.UnitOfWork.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .Returns(context.SaveChangesAsync(It.IsAny<CancellationToken>()));

            SessionService sessionService = new SessionService(
                _sessionRepositoryMock.Object,
                context,
                _serviceConfigurationMock.Object,
                _eventRepositoryMock.Object);

            await sessionService.DeleteAsync("0");

            Assert.That(context.Sessions.Count(), Is.EqualTo(0));
        }

        [Test]
        public async Task DeleteAsync_ExpectedNotFoundException()
        {
            await using IdentityDbContext context = MemoryDbContext.Create(Guid.NewGuid().ToString());

            await FillSessionFor_DeleteAsync(context);

            _sessionRepositoryMock.Setup(x => x.UnitOfWork.SaveChangesAsync(CancellationToken.None))
                .Returns(context.SaveChangesAsync());

            SessionService sessionService = new SessionService(
                _sessionRepositoryMock.Object,
                context,
                _serviceConfigurationMock.Object,
                _eventRepositoryMock.Object);

            RootServiceException exception = Assert.ThrowsAsync<RootServiceException>(async () =>
                await sessionService.DeleteAsync("1"));

            Assert.That(exception.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        private async Task FillSessionFor_DeleteAsync(IdentityDbContext dbContext)
        {
            await dbContext.Sessions.AddAsync(new Session("0", "")
            {
                IsActive = false,
                Updated = DateTime.UtcNow
            });


            await dbContext.SaveChangesAsync();
        }


        private async Task FillSessionFor_GetActiveSessionsAsync_ExpectedQuantity(IdentityDbContext dbContext)
        {
            await dbContext.Sessions.AddAsync(new Session("1", "1")
            {
                IsActive = true,
                Updated = DateTime.UtcNow
            });

            await dbContext.Sessions.AddAsync(new Session("1", "1")
            {
                IsActive = true,
                Updated = DateTime.UtcNow.AddHours(-2)
            });

            await dbContext.Sessions.AddAsync(new Session("1", "1")
            {
                IsActive = false,
                Updated = DateTime.UtcNow
            });

            await dbContext.Sessions.AddAsync(new Session("1", "1")
            {
                IsActive = false,
                Updated = DateTime.UtcNow.AddHours(-2)
            });

            await dbContext.Sessions.AddAsync(new Session("2", "1")
            {
                IsActive = true,
                Updated = DateTime.UtcNow
            });

            await dbContext.Sessions.AddAsync(new Session("2", "2")
            {
                IsActive = true,
                Updated = DateTime.UtcNow
            });

            await dbContext.Sessions.AddAsync(new Session("2", "1")
            {
                IsActive = true,
                Updated = DateTime.UtcNow.AddHours(-2)
            });

            await dbContext.Sessions.AddAsync(new Session("2", "1")
            {
                IsActive = false,
                Updated = DateTime.UtcNow
            });

            await dbContext.Sessions.AddAsync(new Session("2", "1")
            {
                IsActive = false,
                Updated = DateTime.UtcNow.AddHours(-2)
            });


            await dbContext.SaveChangesAsync();
        }
    }
}
