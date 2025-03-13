using MarketToolsV3.ApiGateway.Constant;
using MarketToolsV3.ApiGateway.Services.Interfaces;
using MarketToolsV3.ConfigurationManager.Models;
using Microsoft.Extensions.Options;
using Ocelot.Metadata;
using Ocelot.Middleware;

namespace MarketToolsV3.ApiGateway.Middlewares
{
    [Obsolete]
    public class CookiesInjectMiddleware(RequestDelegate next)
    {
        private static readonly CookieOptions CookieOptions = new()
        { HttpOnly = true, Expires = DateTimeOffset.UtcNow.AddYears(1) };

        public async Task Invoke(HttpContext httpContext,
            IAuthContext authContext,
            IOptions<AuthConfig> options,
            IAccessTokenService accessTokenService
            )
        {
            var skipRefreshCookies = httpContext.Items.DownstreamRoute().GetMetadata<bool>("skip-refresh-cookies");

            if (authContext.State == AuthState.TokensRefreshed
                && string.IsNullOrEmpty(authContext.SessionToken) == false
                && string.IsNullOrEmpty(authContext.AccessToken) == false
                && skipRefreshCookies == false)
            {
                httpContext.Response.Cookies.Append(options.Value.AccessTokenName, authContext.AccessToken, CookieOptions);
                httpContext.Response.Cookies.Append(options.Value.RefreshTokenName, authContext.SessionToken, CookieOptions);
            }

            await next(httpContext);
        }
    }
}
