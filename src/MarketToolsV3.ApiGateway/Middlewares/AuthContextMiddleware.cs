using MarketToolsV3.ApiGateway.Models;
using MarketToolsV3.ApiGateway.Services.Interfaces;
using Microsoft.Extensions.Options;

namespace MarketToolsV3.ApiGateway.Middlewares
{
    public class AuthContextMiddleware(RequestDelegate next)
    {
        public Task Invoke(HttpContext httpContext,
            IAuthContext authContext,
            IOptions<AuthConfiguration> options)
        {
            httpContext.Request.Cookies.TryGetValue(options.Value.AccessTokenName, out string? accessToken);
            httpContext.Request.Cookies.TryGetValue(options.Value.RefreshTokenName, out string? refreshToken);

            authContext.AccessToken = accessToken;
            authContext.SessionToken = refreshToken;

            return next(httpContext);
        }
    }
}
