using MarketToolsV3.ApiGateway.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Primitives;

namespace MarketToolsV3.ApiGateway.Middlewares
{
    public class ModuleCheckMiddleware(RequestDelegate next)
    {
        public async Task Invoke(HttpContext httpContext,
            IAccessTokenContext accessTokenContext)
        {

            if (httpContext.Request.Headers.TryGetValue("moduleId", out var strModuleId)
                && int.TryParse(strModuleId, out var moduleId)
                && accessTokenContext.Data is { ModuleAuthInfo: not null }
                && accessTokenContext.Data.ModuleAuthInfo.Id != moduleId)
            {
                httpContext.Request.Headers.Remove("Authorization");
                accessTokenContext.Remove();
            }

            await next(httpContext);
        }
    }
}
