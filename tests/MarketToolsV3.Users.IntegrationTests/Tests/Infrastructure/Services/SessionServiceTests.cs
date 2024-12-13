using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Identity.Domain.Entities;
using Identity.Domain.Seed;
using Identity.Infrastructure.Database;
using Identity.Infrastructure.Services;
using MarketToolsV3.Users.IntegrationTests.Source;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Moq;

namespace MarketToolsV3.Users.IntegrationTests.Tests.Infrastructure.Services
{
    public class SessionServiceTests
    {
        private Mock<IRepository<Session>> _sessionRepositoryMock;
        private Mock<IOptions<ServiceConfiguration>> _optionsMock;
        private Mock<IEventRepository> _eventRepositoryMock;

        [SetUp]
        public void Setup()
        {
            _sessionRepositoryMock = new Mock<IRepository<Session>>();
            _eventRepositoryMock = new Mock<IEventRepository>();
            _optionsMock = new Mock<IOptions<ServiceConfiguration>>();
        }

        [TestCase("1", 10, 2)]
        [TestCase("1", 3, 2)]
        [TestCase("1", 1, 1)]
        [TestCase("2", 10, 3)]
        [TestCase("2", 5, 3)]
        [TestCase("2", 3, 2)]
        [TestCase("2", 1, 1)]
        public async Task GetActiveSessionsAsync_InputUserIdAndExpireHours_ExpectQuantitySessions(string userId, int expireHours, int expectedQuantity)
        {
            _optionsMock.SetupGet(x => x.Value)
                .Returns(new ServiceConfiguration
                {
                    ExpireRefreshTokenHours = expireHours
                });

            SessionService sessionService = new(
                _sessionRepositoryMock.Object,
                _optionsMock.Object,
                _eventRepositoryMock.Object);

            await using IdentityDbContext memoryDbContext = MemoryDbContextFactory.CreateIdentityContext();

            IEnumerable<Session> sessions = CreateSessions();
            memoryDbContext.Sessions.AddRange(sessions);
            await memoryDbContext.SaveChangesAsync();

            _sessionRepositoryMock.Setup(x => x.AsQueryable())
                .Returns(memoryDbContext.Sessions.AsQueryable());

            IEnumerable<Session> result = await sessionService
                .GetActiveSessionsAsync(userId, It.IsAny<CancellationToken>());

            Assert.That(result.Count(), Is.EqualTo(expectedQuantity));
        }

        private static IEnumerable<Session> CreateSessions()
        {
            return [
                new("1", "1")
                {
                    Updated = DateTime.UtcNow,
                    IsActive = true
                },
                new("1", "1")
                {
                    Updated = DateTime.UtcNow.AddHours(-2),
                    IsActive = true
                },
                new("1", "1")
                {
                    Updated = DateTime.UtcNow,
                    IsActive = false
                },
                new("1", "1")
                {
                    Updated = DateTime.UtcNow.AddHours(-2),
                    IsActive = false
                },
                new("2", "2")
                {
                    Updated = DateTime.UtcNow,
                    IsActive = true
                },
                new("2", "2")
                {
                    Updated = DateTime.UtcNow.AddHours(-2),
                    IsActive = true
                },
                new("2", "2")
                {
                    Updated = DateTime.UtcNow.AddHours(-4),
                    IsActive = true
                },
                new("2", "2")
                {
                    Updated = DateTime.UtcNow,
                    IsActive = false
                },
                new("2", "2")
                {
                    Updated = DateTime.UtcNow.AddHours(-2),
                    IsActive = false
                }
            ];
        }
    }
}
