using Identity.Application.Models;
using Identity.Application.Services;
using Identity.WebApi.Services;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using Identity.WebApi.Models;

namespace Identity.WebApi.Middlewares
{
    public class AuthDetailsMiddleware(RequestDelegate next)
    {
        public async Task Invoke(HttpContext httpContext,
            IAuthContext authContext,
            ITokenService<JwtRefreshTokenDto> refreshTokenService,
            IOptions<WebApiConfiguration> options)
        {
            if (httpContext.Request.Cookies.TryGetValue(options.Value.RefreshTokenName, out string? token)
                && string.IsNullOrEmpty(token) == false
                && await refreshTokenService.IsValid(token))
            {
                JwtRefreshTokenDto tokenData = refreshTokenService.Read(token);
                authContext.SessionId = tokenData.Id;
            }

            authContext.UserId = httpContext.User?.FindFirstValue(ClaimTypes.NameIdentifier);

            await next(httpContext);
        }
    }
}
