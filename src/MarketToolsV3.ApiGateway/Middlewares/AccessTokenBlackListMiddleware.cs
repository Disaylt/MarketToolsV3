using MarketToolsV3.ApiGateway.Services.Interfaces;

namespace MarketToolsV3.ApiGateway.Middlewares
{
    public class AccessTokenBlackListMiddleware(RequestDelegate next)
    {
        public async Task Invoke(HttpContext httpContext,
        ICacheRepository cacheRepository)
        {
            await next(httpContext);
        }
    }
}
