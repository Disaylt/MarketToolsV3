using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Identity.WebApi.Controllers;
using Identity.WebApi.Models;
using Identity.WebApi.Services;
using Identity.WebApi.Services.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Moq;

namespace MarketToolsV3.Users.UnitTests.Tests.WebApi
{
    internal class SessionControllerTests
    {
        private Mock<IMediator> _mediatorMock;
        private Mock<ISessionContextService> _sessionContextServiceMock;

        [SetUp]
        public void Setup()
        {
            _mediatorMock = new Mock<IMediator>();
            _sessionContextServiceMock = new Mock<ISessionContextService>();
        }

        [Test]
        public async Task DeactivateAsync_ReturnOkResult()
        {
            SessionController sessionController = new(_mediatorMock.Object, _sessionContextServiceMock.Object);

            IActionResult result = await sessionController
                .DeactivateAsync(It.IsAny<string>(), It.IsAny<CancellationToken>());

            Assert.That(result as OkResult, Is.Not.Null);
        }
    }
}
