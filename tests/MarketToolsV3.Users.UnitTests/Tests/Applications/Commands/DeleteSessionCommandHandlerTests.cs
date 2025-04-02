using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Identity.Application.Commands;
using Identity.Application.Models;
using Identity.Application.Services.Abstract;
using Identity.Domain.Seed;
using Moq;

namespace MarketToolsV3.Users.UnitTests.Tests.Applications.Commands
{
    internal class DeleteSessionCommandHandlerTests
    {
        private Mock<IStringIdQuickSearchService<SessionDto>> _sessionSearchServiceMock;
        private Mock<ISessionService> _sessionServiceMock;
        private DeactivateSessionCommand _command;

        [SetUp]
        public void Setup()
        {
            _sessionSearchServiceMock = new Mock<IStringIdQuickSearchService<SessionDto>>();
            _sessionServiceMock = new Mock<ISessionService>();
            _command = new DeactivateSessionCommand
            {
                Id = "0"
            };
        }

        [Test]
        public async Task Handle_CallDeleteFromCache()
        {
            DeactivateSessionCommandHandler commandHandler = new(
                _sessionSearchServiceMock.Object,
                _sessionServiceMock.Object);

            await commandHandler.Handle(_command, It.IsAny<CancellationToken>());

            _sessionSearchServiceMock.Verify(x=> 
                    x.ClearAsync(It.IsAny<string>(), CancellationToken.None),
                Times.Once);
        }
    }
}
