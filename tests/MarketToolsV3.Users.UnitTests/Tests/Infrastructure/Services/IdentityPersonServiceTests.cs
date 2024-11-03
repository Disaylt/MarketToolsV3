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
using Identity.Infrastructure.Services;
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
        }

        [Test]
        public async Task AddAsync_ExpectedCallUserManagerCreateAsync()
        {
            _userManagerMock.Setup(x => x.CreateAsync(It.IsAny<IdentityPerson>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            IdentityPersonService identityPersonService = new IdentityPersonService(_userManagerMock.Object,
                _loggerMock.Object, _eventRepositoryMock.Object);

            await identityPersonService.AddAsync(new IdentityPerson(), It.IsAny<string>());

            _userManagerMock.Verify(o => o.CreateAsync(It.IsAny<IdentityPerson>(), It.IsAny<string>()), 
                Times.Exactly(1));
        }

        [Test]
        public void AddAsync_ExpectedBadRequest()
        {
            _userManagerMock.Setup(x => x.CreateAsync(It.IsAny<IdentityPerson>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Failed());

            IdentityPersonService identityPersonService = new IdentityPersonService(_userManagerMock.Object,
                _loggerMock.Object, _eventRepositoryMock.Object);

            RootServiceException exception = Assert.ThrowsAsync<RootServiceException>(async () =>
                await identityPersonService.AddAsync(It.IsAny<IdentityPerson>(), It.IsAny<string>()));

            Assert.That(exception.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        [Test]
        public async Task AddAsync_ExpectedCallEventRepositoryAddNotification()
        {
            _userManagerMock.Setup(x => x.CreateAsync(It.IsAny<IdentityPerson>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            IdentityPersonService identityPersonService = new IdentityPersonService(_userManagerMock.Object,
                _loggerMock.Object, _eventRepositoryMock.Object);

            await identityPersonService.AddAsync(new IdentityPerson(), It.IsAny<string>());

            _eventRepositoryMock.Verify(o=>
                o.AddNotification(It.Is<INotification>(
                    x=> x.GetType() == typeof(IdentityCreated))), 
                Times.Exactly(1));
        }
    }
}
