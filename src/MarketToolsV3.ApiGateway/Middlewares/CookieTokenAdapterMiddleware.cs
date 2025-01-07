using MarketToolsV3.ApiGateway.Models;
using Microsoft.Extensions.Options;

namespace MarketToolsV3.ApiGateway.Middlewares
{
    public class CookieTokenAdapterMiddleware(RequestDelegate next)
    {
        public Task Invoke(HttpContext httpContext, IOptions<AuthConfiguration> options)
        {
            if (httpContext.Request.Cookies.TryGetValue(options.Value.AccessTokenName, out string? token)
                && string.IsNullOrEmpty(token) == false)
            {
                httpContext.Request.Headers.Authorization = $"Bearer {token}";
            }

            return next(httpContext);
        }
    }
}
