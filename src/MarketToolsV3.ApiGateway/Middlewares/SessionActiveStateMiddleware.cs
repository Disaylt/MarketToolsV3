using MarketToolsV3.ApiGateway.Constant;
using MarketToolsV3.ApiGateway.Models;
using MarketToolsV3.ApiGateway.Services.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Proto.Contract.Identity;

namespace MarketToolsV3.ApiGateway.Middlewares;

public class SessionActiveStateMiddleware(RequestDelegate next)
{
    public async Task Invoke(HttpContext httpContext,
        Session.SessionClient sessionClient,
        IOptions<AuthConfiguration> options,
        ICacheRepository<SessionActiveStatusReply> sessionCacheRepository,
        IAuthContext authContext)
    {
        if (authContext is { State: AuthState.AccessTokenValid, SessionId: not null })
        {
            SessionInfoRequest sessionInfoRequest = new SessionInfoRequest
            {
                Id = authContext.SessionId
            };

            SessionActiveStatusReply? sessionState = await sessionCacheRepository.GetAsync(authContext.SessionId);

            if (sessionState is null)
            {
                sessionState = await sessionClient.GetActiveStatusAsync(sessionInfoRequest);
            }

            if (sessionState.IsActive)
            {
                authContext.State = AuthState.SessionActive;
            }
        }

        await next(httpContext);
    }
}