using MarketToolsV3.ApiGateway.Models.Tokens;
using MarketToolsV3.ApiGateway.Services.Interfaces;

namespace MarketToolsV3.ApiGateway.Middlewares
{
    public class AccessTokenContextMiddleware(RequestDelegate next)
    {
        public async Task Invoke(HttpContext httpContext,
            ITokenReader<AccessToken> accessTokenReader,
            IAccessTokenContext accessTokenContext)
        {
            string? authorizationValue = httpContext.Request.Headers.Authorization;

            string token = string.Empty;
            if (authorizationValue != null)
            {
                token = authorizationValue.Replace("Bearer", "").Trim();
            }

            accessTokenContext.Data = accessTokenReader.ReadOrDefault(token);

            await next(httpContext);
        }
    }
}
