using Identity.Application.Models;
using Identity.Application.Services;
using Identity.WebApi.Services;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using Identity.WebApi.Models;
using Microsoft.Extensions.Primitives;

namespace Identity.WebApi.Middlewares
{
    public class AuthDetailsMiddleware(RequestDelegate next)
    {
        public async Task Invoke(HttpContext httpContext,
            IAuthContext authContext,
            ITokenService<JwtAccessTokenDto> accessTokenService,
            IOptions<WebApiConfiguration> options)
        {
            IRequestCookieCollection requestCookieCollection =  httpContext.Request.Cookies;

            if (requestCookieCollection.TryGetValue("", out var accessToken)
                && string.IsNullOrEmpty(accessToken) == false
                && await accessTokenService.IsValid(accessToken))
            {
                JwtAccessTokenDto tokenData = accessTokenService.Read(accessToken);
                authContext.SessionId = tokenData.SessionId;
            }

            authContext.UserId = httpContext.User?.FindFirstValue(ClaimTypes.NameIdentifier);

            await next(httpContext);
        }
    }
}
