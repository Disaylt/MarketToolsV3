using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Identity.Application.Commands;
using Identity.Application.Models;
using Identity.WebApi.Controllers;
using Identity.WebApi.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Moq;

namespace MarketToolsV3.Users.UnitTests.Tests.WebApi
{
    internal class UserControllerTests
    {
        private Mock<IMediator> _mediatorMock;
        private Mock<IOptions<WebApiConfiguration>> _optionsMock;

        [SetUp]
        public void Setup()
        {
            _mediatorMock = new Mock<IMediator>();
            _optionsMock = new Mock<IOptions<WebApiConfiguration>>();
            _optionsMock.SetupGet(x => x.Value)
                .Returns(new WebApiConfiguration());
        }

        [Test]
        public async Task RegisterAsync_ReturnOkAuthDetails()
        {
            NewUserModel body = new NewUserModel
            {
                Email = "email",
                Login = "login",
                Password = "password"
            };

            _mediatorMock.Setup(x => x.Send(It.IsAny<CreateNewUserCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new AuthDetailsDto
                {
                    AuthToken = "",
                    SessionToken = ""
                });

            UserController userController = new UserController(_mediatorMock.Object, _optionsMock.Object);
            userController.ControllerContext.HttpContext = new DefaultHttpContext();

            IActionResult result = await userController.RegisterAsync(body, It.IsAny<CancellationToken>());

            OkObjectResult? objectResult = result as OkObjectResult;

            Assert.That(objectResult!.Value, Is.AssignableTo<AuthDetailsDto>());
        }

        [TestCase("token-a-1", "token-r-1")]
        [TestCase("token-a-2", "token-r-2")]
        [TestCase("token-a-3", "token-r-3")]
        [TestCase("token-a-4", "token-r-4")]
        public async Task RegisterAsync_CheckAuthDetailProperties(string accessToken, string refreshToken)
        {
            NewUserModel body = new NewUserModel
            {
                Email = "email",
                Login = "login",
                Password = "password"
            };

            _mediatorMock.Setup(x => x.Send(It.IsAny<CreateNewUserCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new AuthDetailsDto
                {
                    AuthToken = accessToken,
                    SessionToken = refreshToken
                });

            UserController userController = new UserController(_mediatorMock.Object, _optionsMock.Object);
            userController.ControllerContext.HttpContext = new DefaultHttpContext();

            IActionResult result = await userController.RegisterAsync(body, It.IsAny<CancellationToken>());

            OkObjectResult? objectResult = result as OkObjectResult;
            AuthDetailsDto? authDetails = objectResult?.Value as AuthDetailsDto;

            Assert.Multiple(() =>
            {
                Assert.That(authDetails!.AuthToken, Is.EqualTo(accessToken));
                Assert.That(authDetails!.SessionToken, Is.EqualTo(refreshToken));
            });
        }
    }
}
