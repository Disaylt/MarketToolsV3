using MarketToolsV3.ApiGateway.Constant;
using MarketToolsV3.ApiGateway.Models;
using MarketToolsV3.ApiGateway.Services.Interfaces;
using Microsoft.Extensions.Options;

namespace MarketToolsV3.ApiGateway.Middlewares
{
    [Obsolete]
    public class AccessTokenMiddleware(RequestDelegate next)
    {
        public async Task Invoke(HttpContext httpContext,
            IAuthContext authContext,
            IAccessTokenService accessTokenService
            )
        {
            if (string.IsNullOrEmpty(authContext.AccessToken) == false
                && await accessTokenService.IsValidAsync(authContext.AccessToken))
            {
                authContext.State = AuthState.AccessTokenValid;
            }

            await next(httpContext);
        }
    }
}
