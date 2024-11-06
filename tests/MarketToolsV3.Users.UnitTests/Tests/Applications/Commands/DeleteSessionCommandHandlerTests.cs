using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Identity.Application.Commands;
using Identity.Application.Models;
using Identity.Application.Services;
using Identity.Domain.Seed;
using Moq;

namespace MarketToolsV3.Users.UnitTests.Tests.Applications.Commands
{
    internal class DeleteSessionCommandHandlerTests
    {
        private Mock<ICacheRepository<SessionStatusDto>> _cacheRepositoryMock;
        private Mock<ISessionService> _sessionServiceMock;
        private DeleteSessionCommand _command;

        [SetUp]
        public void Setup()
        {
            _cacheRepositoryMock = new Mock<ICacheRepository<SessionStatusDto>>();
            _sessionServiceMock = new Mock<ISessionService>();
            _command = new DeleteSessionCommand
            {
                Id = "0"
            };
        }

        [Test]
        public async Task Handle_CallDeleteFromCache()
        {
            DeleteSessionCommandHandler commandHandler = new DeleteSessionCommandHandler(
                _cacheRepositoryMock.Object,
                _sessionServiceMock.Object);

            await commandHandler.Handle(_command, It.IsAny<CancellationToken>());

            _cacheRepositoryMock.Verify(x=> 
                    x.DeleteAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()),
                Times.Once);
        }
    }
}
