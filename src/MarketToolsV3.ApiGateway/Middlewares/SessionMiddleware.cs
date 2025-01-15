using MarketToolsV3.ApiGateway.Constant;
using MarketToolsV3.ApiGateway.Models;
using MarketToolsV3.ApiGateway.Services.Interfaces;
using MarketToolsV3.ConfigurationManager.Models;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Proto.Contract.Identity;

namespace MarketToolsV3.ApiGateway.Middlewares;

public class SessionMiddleware(RequestDelegate next)
{
    public async Task Invoke(HttpContext httpContext,
        Session.SessionClient sessionClient,
        IOptions<AuthConfig> options,
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
                await sessionCacheRepository.SetAsync(authContext.SessionId, sessionState, TimeSpan.FromHours(1));
            }

            if (sessionState.IsActive)
            {
                authContext.State = AuthState.SessionActive;
            }
        }

        await next(httpContext);

        string sessionRemoveHeaderName = options.Value.Headers.SessionRemove.Name;
        string? removeSessionId = httpContext.Response.Headers[sessionRemoveHeaderName].FirstOrDefault();

        if (string.IsNullOrEmpty(removeSessionId) == false)
        {
            await sessionCacheRepository.DeleteAsync(removeSessionId, CancellationToken.None);
        }
    }
}