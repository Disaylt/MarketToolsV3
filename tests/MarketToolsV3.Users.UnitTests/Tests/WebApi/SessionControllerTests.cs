using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Identity.WebApi.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace MarketToolsV3.Users.UnitTests.Tests.WebApi
{
    internal class SessionControllerTests
    {
        private Mock<IMediator> _mediatorMock;

        [SetUp]
        public void Setup()
        {
            _mediatorMock = new Mock<IMediator>();
        }

        [Test]
        public async Task DeactivateAsync_ReturnOkResult()
        {
            SessionController sessionController = new(_mediatorMock.Object);

            IActionResult result = await sessionController
                .DeactivateAsync(It.IsAny<string>(), It.IsAny<CancellationToken>());

            Assert.That(result as OkResult, Is.Not.Null);
        }
    }
}
