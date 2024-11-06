using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Identity.Application.Models;
using Identity.Application.Queries;
using Identity.Application.Services;
using Identity.Domain.Entities;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.DataCollection;
using Moq;

namespace MarketToolsV3.Users.UnitTests.Tests.Applications.Queries
{
    internal class GetActiveSessionsQueryHandlerTests
    {
        private Mock<ISessionService> _sessionServiceMock;
        private IEnumerable<Session> _testSessions;

        [SetUp]
        public void Setup()
        {
            _sessionServiceMock = new Mock<ISessionService>();
            _testSessions =
            [
                new("1", "1"),
                new("2", "2"),
                new("3", "3"),
                new("4", "4")
            ];
        }

        [Test]
        public async Task Handle_ContainsSessionWithCurrentStatus()
        {
            _sessionServiceMock.Setup(x =>
                    x.GetActiveSessionsAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(_testSessions);

            GetActiveSessionsQuery query = new()
            {
                CurrentSessionId = _testSessions.First().Id,
                UserId = string.Empty
            };

            GetActiveSessionsQueryHandler queryHandler = new(_sessionServiceMock.Object);

            IEnumerable<SessionDto> sessions = await queryHandler.Handle(query, It.IsAny<CancellationToken>());

            Assert.That(sessions.Any(x => x.IsCurrent), Is.True);
        }

        [Test]
        public async Task Handle_NotContainsSessionWithCurrentStatus()
        {
            _sessionServiceMock.Setup(x =>
                    x.GetActiveSessionsAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(_testSessions);

            GetActiveSessionsQuery query = new()
            {
                UserId = string.Empty
            };

            GetActiveSessionsQueryHandler queryHandler = new(_sessionServiceMock.Object);

            IEnumerable<SessionDto> sessions = await queryHandler.Handle(query, It.IsAny<CancellationToken>());

            Assert.That(sessions.Any(x => x.IsCurrent), Is.False);
        }

        [TestCase("1")]
        [TestCase("2")]
        [TestCase("3")]
        public async Task Handle_ReturnQuantityCurrentSession(string identityId)
        {
            _sessionServiceMock.Setup(x =>
                    x.GetActiveSessionsAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(_testSessions);

            GetActiveSessionsQuery query = new()
            {
                CurrentSessionId = _testSessions.First(x=> x.IdentityId == identityId).Id,
                UserId = string.Empty
            };

            GetActiveSessionsQueryHandler queryHandler = new(_sessionServiceMock.Object);

            IEnumerable<SessionDto> sessions = await queryHandler.Handle(query, It.IsAny<CancellationToken>());

            Assert.That(sessions.Count(x=> x.IsCurrent), Is.EqualTo(1));
        }

        [Test]
        public async Task Handle_CheckMapping()
        {
            Session session = new("test-identity-1", "test-agent-1");
            _sessionServiceMock.Setup(x =>
                    x.GetActiveSessionsAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync([session]);

            GetActiveSessionsQuery query = new()
            {
                UserId = string.Empty
            };

            GetActiveSessionsQueryHandler queryHandler = new(_sessionServiceMock.Object);

            IEnumerable<SessionDto> sessions = await queryHandler.Handle(query, It.IsAny<CancellationToken>());
            SessionDto result = sessions.First();

            Assert.Multiple(() =>
            {
                Assert.That(result.CreateDate, Is.EqualTo(session.Created));
                Assert.That(result.Id, Is.EqualTo(session.Id));
                Assert.That(result.Updated, Is.EqualTo(session.Updated));
                Assert.That(result.UserAgent, Is.EqualTo(session.UserAgent));
            });
        }
    }
}
