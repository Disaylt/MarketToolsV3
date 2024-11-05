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
                _serviceConfigurationMock.Object,
                _eventRepositoryMock.Object);

            await sessionService.UpdateAsync(session, token, userAgent);

            Assert.That(userAgent, Is.EqualTo(session.UserAgent));
            Assert.That(token, Is.EqualTo(session.Token));

            _sessionRepositoryMock.Verify(x => x.UnitOfWork.SaveEntitiesAsync(default), Times.Once());
        }
    }
}
