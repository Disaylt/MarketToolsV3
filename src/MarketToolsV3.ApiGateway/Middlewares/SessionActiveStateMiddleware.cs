using MarketToolsV3.ApiGateway.Models;
using MarketToolsV3.ApiGateway.Services;
using Microsoft.Extensions.Options;
using Proto.Contract.Identity;

namespace MarketToolsV3.ApiGateway.Middlewares;

public class SessionActiveStateMiddleware(RequestDelegate next)
{
    public async Task Invoke(HttpContext httpContext,
        Session.SessionClient sessionClient,
        IOptions<AuthConfiguration> options,
        IAuthContext authContext)
    {
        if (authContext.IsAuth && string.IsNullOrEmpty(authContext.SessionToken) == false)
        {
            SessionInfoRequest sessionInfoRequest = new SessionInfoRequest
            {
                Token = authContext.SessionToken
            };

            SessionActiveStatusReply response = await sessionClient.GetActiveStatusAsync(sessionInfoRequest);

            authContext.IsAuth = response.IsActive;
        }

        await next(httpContext);
    }
}