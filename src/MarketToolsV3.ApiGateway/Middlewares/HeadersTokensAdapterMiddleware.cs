using MarketToolsV3.ApiGateway.Models;
using MarketToolsV3.ApiGateway.Services;
using Microsoft.Extensions.Options;
using static Microsoft.IO.RecyclableMemoryStreamManager;

namespace MarketToolsV3.ApiGateway.Middlewares
{
    public class HeadersTokensAdapterMiddleware(RequestDelegate next)
    {
        public Task Invoke(HttpContext httpContext, 
            IAuthContext authContext)
        {
            if (authContext.IsAuth)
            {
                httpContext.Request.Headers.Authorization = $"Bearer {authContext.AccessToken}";
                httpContext.Request.Headers.Append("Session", authContext.SessionToken);
            }

            return next(httpContext);
        }
    }
}
