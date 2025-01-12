using System.Text.Json;
using Google.Protobuf.WellKnownTypes;
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
            bool isContainsAccessToken = 
                httpContext.Request.Cookies.TryGetValue(options.Value.AccessTokenName, out string? accessToken);
            bool isContainsRefreshToken =
                httpContext.Request.Cookies.TryGetValue(options.Value.RefreshTokenName, out string? refreshToken);

            if (
                (isContainsAccessToken && string.IsNullOrEmpty(accessToken) == false)
                || 
                (isContainsRefreshToken && string.IsNullOrEmpty(refreshToken) == false))
            {
                AuthInfoRequest request = new AuthInfoRequest
                {
                    UserAgent = httpContext.Request.Headers.UserAgent.FirstOrDefault() ?? "Неизвестное устройство",
                    Details = new AuthInfoRequest.Types.Details
                    {
                        AuthToken = accessToken ?? string.Empty,
                        SessionToken = refreshToken ?? string.Empty
                    }
                };

                AuthInfoReply response = await authClient.GetAuthInfoAsync(request);

                if (response.IsValid)
                {
                    authContext.AccessToken = accessToken;
                    authContext.SessionToken = refreshToken;
                }

                if (response.Refreshed & response.HasDetails & response.IsValid)
                {
                    authContext.AccessToken = response.Details.AuthToken;
                    authContext.SessionToken = response.Details.SessionToken;
                }

                authContext.IsAuth = response.IsValid;
                authContext.Refreshed = response.Refreshed;
            }

            await next(httpContext);

            if (authContext.Refreshed & authContext.IsAuth)
            {
                if (authContext.AccessToken != null)
                {
                    httpContext.Response.Cookies.Append(options.Value.AccessTokenName, authContext.AccessToken, CookieOptions);
                }

                if (authContext.SessionToken != null)
                {
                    httpContext.Response.Cookies.Append(options.Value.RefreshTokenName, authContext.SessionToken, CookieOptions);
                }
            }
        }
    }
}
