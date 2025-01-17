﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Identity.Application.Commands;
using Identity.Application.Models;
using Identity.WebApi.Controllers;
using Identity.WebApi.Models;
using Identity.WebApi.Services;
using Identity.WebApi.Services.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Moq;

namespace MarketToolsV3.Users.UnitTests.Tests.WebApi
{
    internal class UserControllerTests
    {
        //to do - add tests to check if cookies were added

        private Mock<IMediator> _mediatorMock;
        private Mock<IAuthContext> _authContextMock;
        private Mock<ICookiesContextService> _cookiesContextServiceMock;
        private Mock<ICredentialsService> _credentialsServiceMock;

        [SetUp]
        public void Setup()
        {
            _mediatorMock = new Mock<IMediator>();
            _cookiesContextServiceMock = new Mock<ICookiesContextService>();
            _credentialsServiceMock = new Mock<ICredentialsService>();
            _authContextMock = new Mock<IAuthContext>();
        }

        [Test]
        public async Task RegisterAsync_ReturnOkAuthDetails()
        {
            NewUserModel body = new()
            {
                Email = "email",
                Login = "login",
                Password = "password"
            };

            _mediatorMock.Setup(x => x.Send(It.IsAny<CreateNewUserCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new AuthResultDto()
                {
                    AuthDetails = new()
                    {
                        AuthToken = "",
                        SessionToken = ""
                    },
                    IdentityDetails = new IdentityDetailsDto()
                    {
                        Email = "",
                        Id = "",
                        Name = ""
                    }
                });

            UserController userController = new(_mediatorMock.Object, _credentialsServiceMock.Object,  _authContextMock.Object)
            {
                ControllerContext =
                {
                    HttpContext = new DefaultHttpContext()
                }
            };

            IActionResult result = await userController.RegisterAsync(body, It.IsAny<CancellationToken>());

            OkObjectResult? objectResult = result as OkObjectResult;

            Assert.That(objectResult!.Value, Is.AssignableTo<AuthResultDto>());
        }

        [TestCase("token-a-1", "token-r-1")]
        [TestCase("token-a-2", "token-r-2")]
        [TestCase("token-a-3", "token-r-3")]
        [TestCase("token-a-4", "token-r-4")]
        public async Task RegisterAsync_CheckAuthDetailProperties(string accessToken, string refreshToken)
        {
            NewUserModel body = new()
            {
                Email = "email",
                Login = "login",
                Password = "password"
            };

            _mediatorMock.Setup(x => x.Send(It.IsAny<CreateNewUserCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new AuthResultDto()
                {
                    AuthDetails = new()
                    {
                        AuthToken = accessToken,
                        SessionToken = refreshToken
					},
                    IdentityDetails = new IdentityDetailsDto()
                    {
                        Email = "",
                        Id = "",
                        Name = ""
                    }
                });

            UserController userController = new(_mediatorMock.Object, _credentialsServiceMock.Object, _authContextMock.Object)
            {
                ControllerContext =
                {
                    HttpContext = new DefaultHttpContext()
                }
            };

            IActionResult result = await userController.RegisterAsync(body, It.IsAny<CancellationToken>());

            OkObjectResult? objectResult = result as OkObjectResult;
			AuthResultDto? authDetails = objectResult?.Value as AuthResultDto;

            Assert.Multiple(() =>
            {
                Assert.That(authDetails!.AuthDetails.AuthToken, Is.EqualTo(accessToken));
                Assert.That(authDetails!.AuthDetails.SessionToken, Is.EqualTo(refreshToken));
            });
        }

        [Test]
        public async Task LoginAsync_ReturnOkAuthDetails()
        {
            LoginModel body = new ()
            {
                Email = "email",
                Password = "password"
            };

            _mediatorMock.Setup(x => x.Send(It.IsAny<LoginCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new AuthResultDto()
                {
                    AuthDetails = new()
                    {
                        AuthToken = "",
                        SessionToken = ""
                    },
                    IdentityDetails = new IdentityDetailsDto()
                    {
                        Email = "",
                        Id = "",
                        Name = ""
                    }
                });

            UserController userController = new(_mediatorMock.Object, _credentialsServiceMock.Object, _authContextMock.Object)
            {
                ControllerContext =
                {
                    HttpContext = new DefaultHttpContext()
                }
            };

            IActionResult result = await userController.LoginAsync(body, It.IsAny<CancellationToken>());

            OkObjectResult? objectResult = result as OkObjectResult;

            Assert.That(objectResult!.Value, Is.AssignableTo<AuthResultDto>());
        }

        [TestCase("token-a-1", "token-r-1")]
        [TestCase("token-a-2", "token-r-2")]
        [TestCase("token-a-3", "token-r-3")]
        [TestCase("token-a-4", "token-r-4")]
        public async Task LoginAsync_CheckAuthDetailProperties(string accessToken, string refreshToken)
        {
            LoginModel body = new()
            {
                Email = "email",
                Password = "password"
            };

            _mediatorMock.Setup(x => x.Send(It.IsAny<LoginCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new AuthResultDto()
                {
                    AuthDetails = new()
                    {
                        AuthToken = accessToken,
                        SessionToken = refreshToken
					},
                    IdentityDetails = new IdentityDetailsDto()
                    {
                        Email = "",
                        Id = "",
                        Name = ""
                    }
                });

            UserController userController = new(_mediatorMock.Object, _credentialsServiceMock.Object, _authContextMock.Object)
            {
                ControllerContext =
                {
                    HttpContext = new DefaultHttpContext()
                }
            };

            IActionResult result = await userController.LoginAsync(body, It.IsAny<CancellationToken>());

            OkObjectResult? objectResult = result as OkObjectResult;
			AuthResultDto? authDetails = objectResult?.Value as AuthResultDto;

            Assert.Multiple(() =>
            {
                Assert.That(authDetails!.AuthDetails.AuthToken, Is.EqualTo(accessToken));
                Assert.That(authDetails!.AuthDetails.SessionToken, Is.EqualTo(refreshToken));
            });
        }
    }
}
