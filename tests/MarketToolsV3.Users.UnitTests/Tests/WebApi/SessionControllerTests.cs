using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Identity.WebApi.Controllers;
using Identity.WebApi.Models;
using Identity.WebApi.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Moq;

namespace MarketToolsV3.Users.UnitTests.Tests.WebApi
{
    internal class SessionControllerTests
    {
        private Mock<IMediator> _mediatorMock;
        private Mock<IOptions<WebApiConfiguration>> _webApiConfigurationMock;

        [SetUp]
        public void Setup()
        {
            _mediatorMock = new Mock<IMediator>();
            _webApiConfigurationMock = new Mock<IOptions<WebApiConfiguration>>();
        }

        [Test]
        public async Task DeactivateAsync_ReturnOkResult()
        {
            SessionController sessionController = new(_mediatorMock.Object, _webApiConfigurationMock.Object);

            IActionResult result = await sessionController
                .DeactivateAsync(It.IsAny<string>(), It.IsAny<CancellationToken>());

            Assert.That(result as OkResult, Is.Not.Null);
        }
    }
}
