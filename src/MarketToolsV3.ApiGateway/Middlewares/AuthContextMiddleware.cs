using MarketToolsV3.ApiGateway.Models;
using MarketToolsV3.ApiGateway.Models.Tokens;
using MarketToolsV3.ApiGateway.Services.Interfaces;
using MarketToolsV3.ConfigurationManager.Models;
using Microsoft.Extensions.Options;

namespace MarketToolsV3.ApiGateway.Middlewares
{
    [Obsolete]
    public class AuthContextMiddleware(RequestDelegate next)
    {
        public Task Invoke(HttpContext httpContext,
            IAuthContext authContext,
            IOptions<AuthConfig> options,
            ITokenReader<AccessToken> tokenReader)
        {
            httpContext.Request.Cookies.TryGetValue(options.Value.AccessTokenName, out string? accessToken);
            httpContext.Request.Cookies.TryGetValue(options.Value.RefreshTokenName, out string? refreshToken);

            authContext.AccessToken = accessToken;
            authContext.SessionToken = refreshToken;

            if (authContext.AccessToken != null)
            {
                authContext.SessionId = tokenReader
                    .ReadOrDefault(authContext.AccessToken)?
                    .SessionId;
            }

            return next(httpContext);
        }
    }
}
