using System.Text.Json;
using Google.Protobuf.WellKnownTypes;
using MarketToolsV3.ApiGateway.Constant;
using MarketToolsV3.ApiGateway.Models;
using MarketToolsV3.ApiGateway.Services.Interfaces;
using MarketToolsV3.ConfigurationManager.Models;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Ocelot.Responses;
using Proto.Contract.Identity;
using static Microsoft.IO.RecyclableMemoryStreamManager;

namespace MarketToolsV3.ApiGateway.Middlewares
{
    public class TokensRefreshMiddleware(RequestDelegate next)
    {
        private static readonly CookieOptions CookieOptions = new()
            { HttpOnly = true, Expires = DateTimeOffset.UtcNow.AddYears(1) };

        public async Task Invoke(HttpContext httpContext, 
            IOptions<AuthConfig> options,
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

                TryRefreshAuthContext(authContext, response);
            }

            await next(httpContext);

            bool containsNewCookie = ContainsNewCookie(options.Value, httpContext);

            if (authContext.State == AuthState.TokensRefreshed 
                && containsNewCookie == false
                && string.IsNullOrEmpty(authContext.AccessToken) == false
                && string.IsNullOrEmpty(authContext.SessionToken) == false)
            {
                httpContext.Response.Cookies.Append(options.Value.AccessTokenName, authContext.AccessToken, CookieOptions);
                httpContext.Response.Cookies.Append(options.Value.RefreshTokenName, authContext.SessionToken, CookieOptions);
            }
        }

        private void TryRefreshAuthContext(IAuthContext authContext, AuthInfoReply authInfoReply)
        {
            if (authInfoReply.HasDetails & authInfoReply.IsValid)
            {
                authContext.AccessToken = authInfoReply.Details.AuthToken;
                authContext.SessionToken = authInfoReply.Details.SessionToken;

                authContext.State = AuthState.TokensRefreshed;
            }
        }

        private bool ContainsNewCookie(AuthConfig authConfig, HttpContext httpContext)
        {
            CookieActionHeader cookieActionHeader = authConfig.Headers.CookieAction;

            return httpContext
                .Response
                .Headers[cookieActionHeader.Name]
                .Contains(cookieActionHeader.Properties.New);
        }
    }
}
