using System.Text.Json;
using Google.Protobuf.WellKnownTypes;
using MarketToolsV3.ApiGateway.Constant;
using MarketToolsV3.ApiGateway.Models;
using MarketToolsV3.ApiGateway.Services.Interfaces;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Ocelot.Responses;
using Proto.Contract.Identity;

namespace MarketToolsV3.ApiGateway.Middlewares
{
    public class TokensRefreshMiddleware(RequestDelegate next)
    {
        private static readonly CookieOptions CookieOptions = new()
            { HttpOnly = true, Expires = DateTimeOffset.UtcNow.AddYears(1) };

        public async Task Invoke(HttpContext httpContext, 
            IOptions<AuthConfiguration> options,
            Auth.AuthClient authClient,
            IAuthContext authContext)
        {
            if (string.IsNullOrEmpty(authContext.SessionToken) == false && authContext.State == AuthState.None)
            {
                AuthInfoRequest request = new AuthInfoRequest
                {
                    UserAgent = httpContext.Request.Headers.UserAgent.FirstOrDefault() ?? "Неизвестное устройство",
                    SessionToken = authContext.SessionToken
                };

                AuthInfoReply response = await authClient.GetAuthInfoAsync(request);

                if (response.HasDetails & response.IsValid)
                {
                    authContext.AccessToken = response.Details.AuthToken;
                    authContext.SessionToken = response.Details.SessionToken;

                    authContext.State = AuthState.TokensRefreshed;
                }
            }

            await next(httpContext);
        }
    }
}
