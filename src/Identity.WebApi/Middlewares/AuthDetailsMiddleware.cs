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
            ITokenService<JwtRefreshTokenDto> refreshTokenService,
            IOptions<WebApiConfiguration> options)
        {
            
            bool isContainsSessionHeader = httpContext.Request.Headers.TryGetValue("Session", out StringValues sessionHeaderValue);
            string? sessionToken = sessionHeaderValue.FirstOrDefault();

            if (isContainsSessionHeader
                && string.IsNullOrEmpty(sessionToken) == false
                && await refreshTokenService.IsValid(sessionToken))
            {
                JwtRefreshTokenDto tokenData = refreshTokenService.Read(sessionToken);
                authContext.SessionId = tokenData.Id;
            }

            authContext.UserId = httpContext.User?.FindFirstValue(ClaimTypes.NameIdentifier);

            await next(httpContext);
        }
    }
}
