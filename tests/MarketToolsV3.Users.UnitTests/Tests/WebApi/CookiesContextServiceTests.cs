using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using Identity.WebApi.Services.Implementation;
using MarketToolsV3.ConfigurationManager.Models;

namespace MarketToolsV3.Users.UnitTests.Tests.WebApi
{
    [TestFixture]
    public class CookiesContextServiceTests
    {
        private Mock<IHttpContextAccessor> _httpContextAccessorMock;
        private Mock<IResponseCookies> _responseCookiesMock;
        private Mock<IHeaderDictionary> _responseHeadersMock;

        [SetUp]
        public void SetUp()
        {
            _httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            _responseCookiesMock = new Mock<IResponseCookies>();
            _responseHeadersMock = new Mock<IHeaderDictionary>();
        }

        [Test]
        public void AddAccessToken_ShouldAddAccessTokenCookie()
        {
            var responseMock = new Mock<HttpResponse>();
            responseMock.SetupGet(r => r.Cookies).Returns(_responseCookiesMock.Object);

            var httpContextMock = new Mock<HttpContext>();
            httpContextMock.SetupGet(c => c.Response).Returns(responseMock.Object);
            _httpContextAccessorMock.Setup(x => x.HttpContext).Returns(httpContextMock.Object);

            var authConfigOptions = Options.Create(new AuthConfig());
            var service = new CookiesContextService(_httpContextAccessorMock.Object, authConfigOptions);

            service.AddAccessToken(It.IsAny<string>());

            _responseCookiesMock.Verify(
                x => x.Append(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CookieOptions>()),
                Times.Once,
                "Access token was not added correctly.");
        }

        [Test]
        public void AddSessionToken_ShouldAddSessionTokenCookie()
        {
            var responseMock = new Mock<HttpResponse>();
            responseMock.SetupGet(r => r.Cookies).Returns(_responseCookiesMock.Object);

            var httpContextMock = new Mock<HttpContext>();
            httpContextMock.SetupGet(c => c.Response).Returns(responseMock.Object);
            _httpContextAccessorMock.Setup(x => x.HttpContext).Returns(httpContextMock.Object);

            var authConfigOptions = Options.Create(new AuthConfig());
            var service = new CookiesContextService(_httpContextAccessorMock.Object, authConfigOptions);

            service.AddSessionToken("test-session-token");

            _responseCookiesMock.Verify(
                x => x.Append("refresh-token", "test-session-token", It.IsAny<CookieOptions>()),
                Times.Once,
                "Refresh token was not added correctly.");
        }

        [Test]
        public void DeleteAccessToken_ShouldRemoveAccessTokenCookie()
        {
            var responseMock = new Mock<HttpResponse>();
            responseMock.SetupGet(r => r.Cookies).Returns(_responseCookiesMock.Object);

            var httpContextMock = new Mock<HttpContext>();
            httpContextMock.SetupGet(c => c.Response).Returns(responseMock.Object);
            _httpContextAccessorMock.Setup(x => x.HttpContext).Returns(httpContextMock.Object);

            var authConfigOptions = Options.Create(new AuthConfig());
            var service = new CookiesContextService(_httpContextAccessorMock.Object, authConfigOptions);

            service.DeleteAccessToken();

            _responseCookiesMock.Verify(
                x => x.Delete("access-token"),
                Times.Once,
                "Access token was not removed correctly.");
        }

        [Test]
        public void DeleteSessionToken_ShouldRemoveSessionTokenCookie()
        {
            var responseMock = new Mock<HttpResponse>();
            responseMock.SetupGet(r => r.Cookies).Returns(_responseCookiesMock.Object);

            var httpContextMock = new Mock<HttpContext>();
            httpContextMock.SetupGet(c => c.Response).Returns(responseMock.Object);
            _httpContextAccessorMock.Setup(x => x.HttpContext).Returns(httpContextMock.Object);

            var authConfigOptions = Options.Create(new AuthConfig());
            var service = new CookiesContextService(_httpContextAccessorMock.Object, authConfigOptions);

            service.DeleteSessionToken();

            _responseCookiesMock.Verify(
                x => x.Delete("refresh-token"),
                Times.Once,
                "Refresh token was not removed correctly.");
        }

        [Test]
        public void MarkAsNew_ShouldAddCorrectHeader()
        {
            var responseMock = new Mock<HttpResponse>();
            responseMock.SetupGet(r => r.Headers).Returns(_responseHeadersMock.Object);

            var httpContextMock = new Mock<HttpContext>();
            httpContextMock.SetupGet(c => c.Response).Returns(responseMock.Object);
            _httpContextAccessorMock.Setup(x => x.HttpContext).Returns(httpContextMock.Object);

            var authConfigOptions = Options.Create(new AuthConfig());
            var service = new CookiesContextService(_httpContextAccessorMock.Object, authConfigOptions);

            service.MarkAsNew();

            _responseHeadersMock.VerifySet(
                h => h["mp-cookie-action"] = "new",
                Times.Once,
                "Header was not added correctly.");
        }
    }
}
