using System.Text.Json;
using Google.Protobuf.WellKnownTypes;
using MarketToolsV3.ApiGateway.Models;
using MarketToolsV3.ApiGateway.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
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
                        AuthToken = accessToken,
                        SessionToken = refreshToken
                    }
                };

                AuthInfoReply response = await authClient.GetAuthInfoAsync(request);

                if (response.IsValid)
                {
                    authContext.AccessToken = accessToken;
                    authContext.SessionToken = refreshToken;
                    authContext.IsAuth = true;
                }

                if (response.Refreshed & response.HasDetails & response.IsValid)
                {
                    httpContext.Response.Cookies.Append(options.Value.AccessTokenName, response.Details.AuthToken, CookieOptions);
                    httpContext.Response.Cookies.Append(options.Value.RefreshTokenName, response.Details.SessionToken, CookieOptions);

                    authContext.AccessToken = response.Details.AuthToken;
                    authContext.SessionToken = response.Details.SessionToken;
                }

            }

            await next(httpContext);
        }
    }
}
