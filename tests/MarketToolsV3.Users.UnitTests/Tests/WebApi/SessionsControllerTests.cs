using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Identity.Application.Models;
using Identity.WebApi.Controllers;
using Identity.WebApi.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace MarketToolsV3.Users.UnitTests.Tests.WebApi
{
    internal class SessionsControllerTests
    {
        private Mock<IMediator> _mediatorMock;
        private Mock<IAuthContext> _authContextMock;

        [SetUp]
        public void Setup()
        {
            _authContextMock = new Mock<IAuthContext>();
            _mediatorMock = new Mock<IMediator>();
        }

        [Test]
        public async Task GetAsync_ReturnOkResult()
        {
            SessionsController sessionsController = new(
                _mediatorMock.Object,
                _authContextMock.Object);

            IActionResult result = await sessionsController.GetAsync(It.IsAny<CancellationToken>());

            Assert.That(result as OkObjectResult, Is.Not.Null);
        }

        [Test]
        public async Task GetAsync_ReturnSessionsType()
        {
            SessionsController sessionsController = new(
                _mediatorMock.Object,
                _authContextMock.Object);

            IActionResult result = await sessionsController.GetAsync(It.IsAny<CancellationToken>());
            OkObjectResult? objectResult = result as OkObjectResult;

            Assert.That(objectResult!.Value, Is.AssignableTo<IEnumerable<SessionDto>>());
        }
    }
}
