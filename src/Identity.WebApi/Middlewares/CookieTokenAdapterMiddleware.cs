using Identity.WebApi.Models;
using Microsoft.Extensions.Options;

namespace Identity.WebApi.Middlewares
{
    public class CookieTokenAdapterMiddleware(RequestDelegate next)
    {
        public Task Invoke(HttpContext httpContext, IOptions<WebApiConfiguration> options)
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
