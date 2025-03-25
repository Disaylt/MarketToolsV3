using MarketToolsV3.ApiGateway.Models.Tokens;
using MarketToolsV3.ApiGateway.Services.Interfaces;

namespace MarketToolsV3.ApiGateway.Middlewares
{
    public class AccessTokenBlackListMiddleware(RequestDelegate next)
    {
        public async Task Invoke(HttpContext httpContext,
        ICacheRepository cacheRepository,
        IAccessTokenContext accessTokenContext)
        {
            if (accessTokenContext.Data == null
                || httpContext.User.Identity?.IsAuthenticated == false
                || await cacheRepository.GetAsync<object>($"blacklist-access-token-{accessTokenContext.Data.Id}") == null)
            {
                httpContext.Request.Headers.Remove("Authorization");
                accessTokenContext.Remove();
            }

            await next(httpContext);
        }
    }
}
