using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Castle.Core.Logging;
using Identity.Domain.Entities;
using Identity.Domain.Events;
using Identity.Domain.Seed;
using Identity.Infrastructure.Services.Implementation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Moq;

namespace MarketToolsV3.Users.UnitTests.Tests.Infrastructure.Services
{
    public class IdentityPersonServiceTests
    {
        private Mock<UserManager<IdentityPerson>> _userManagerMock;
        private Mock<IEventRepository> _eventRepositoryMock;
        private Mock<ILogger<IdentityPersonService>> _loggerMock;
        private Mock<IRepository<IdentityPerson>> _repositoryMock;

        [SetUp]
        public void SetUp()
        {
            _userManagerMock = new Mock<UserManager<IdentityPerson>>(
                new Mock<IUserStore<IdentityPerson>>().Object,
                null!,
                null!,
                null!,
                null!,
                null!,
                null!,
                null!,
                null!);
            _eventRepositoryMock = new Mock<IEventRepository>();
            _loggerMock = new Mock<ILogger<IdentityPersonService>>();
            _repositoryMock = new Mock<IRepository<IdentityPerson>>();
            _repositoryMock.Setup(x => x.UnitOfWork).Returns(new Mock<IUnitOfWork>().Object);
        }

        [Test]
        public async Task AddAsync_CallCreateAsync()
        {
            IdentityPersonService identityPersonService = new(_userManagerMock.Object,
                _repositoryMock.Object,
                _loggerMock.Object,
                _eventRepositoryMock.Object);

            _userManagerMock.Setup(x => 
                    x.CreateAsync(It.IsAny<IdentityPerson>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            await identityPersonService.AddAsync(new IdentityPerson(), "");

            _userManagerMock.Verify(x=> x.CreateAsync(It.IsAny<IdentityPerson>(), It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void AddAsync_ReturnExeptionWithBadRequestStatusCode()
        {
            IdentityPersonService identityPersonService = new(_userManagerMock.Object,
                _repositoryMock.Object,
                _loggerMock.Object,
                _eventRepositoryMock.Object);

            _userManagerMock.Setup(x =>
                    x.CreateAsync(It.IsAny<IdentityPerson>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Failed());

            RootServiceException ex = Assert.CatchAsync<RootServiceException>(() => identityPersonService.AddAsync(new IdentityPerson(), ""));

            Assert.That(ex.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        [Test]
        public async Task AddAsync_CallAddNotification()
        {
            IdentityPersonService identityPersonService = new(_userManagerMock.Object,
                _repositoryMock.Object,
                _loggerMock.Object,
                _eventRepositoryMock.Object);

            _userManagerMock.Setup(x =>
                    x.CreateAsync(It.IsAny<IdentityPerson>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            await identityPersonService.AddAsync(new(), "");

            _eventRepositoryMock.Verify(x=>
                    x.AddNotification(It.IsAny<IdentityCreated>()),
                Times.Once);
        }

        [Test]
        public async Task AddAsync_CallSaveEntitiesAsync()
        {
            IdentityPersonService identityPersonService = new(_userManagerMock.Object,
                _repositoryMock.Object,
                _loggerMock.Object,
                _eventRepositoryMock.Object);

            _userManagerMock.Setup(x =>
                    x.CreateAsync(It.IsAny<IdentityPerson>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            await identityPersonService.AddAsync(new IdentityPerson(), "");

            _repositoryMock.Verify(x=> 
                    x.UnitOfWork.SaveEntitiesAsync(It.IsAny<CancellationToken>()),
                Times.Once);
        }

        [Test]
        public async Task CheckPassword_CallCheckPasswordAsync()
        {
            IdentityPersonService identityPersonService = new(_userManagerMock.Object,
                _repositoryMock.Object,
                _loggerMock.Object,
                _eventRepositoryMock.Object);

            await identityPersonService.CheckPassword(new IdentityPerson(), It.IsAny<string>());

            _userManagerMock.Verify(x => 
                    x.CheckPasswordAsync(It.IsAny<IdentityPerson>(), It.IsAny<string>()),
                Times.Once);
        }

        [Test]
        public async Task FindByEmailAsync_CallFindEmailAsync()
        {
            IdentityPersonService identityPersonService = new(_userManagerMock.Object,
                _repositoryMock.Object,
                _loggerMock.Object,
                _eventRepositoryMock.Object);

            await identityPersonService.FindByEmailAsync(string.Empty);

            _userManagerMock.Verify(x =>
                    x.FindByEmailAsync(It.IsAny<string>()),
                Times.Once);
        }
    }
}
