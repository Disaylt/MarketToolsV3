using Identity.WebApi.Services.Interfaces;
using MarketToolsV3.ConfigurationManager.Models;
using Microsoft.Extensions.Options;

namespace Identity.WebApi.Services.Implementation
{
    public class CookiesContextService(IHttpContextAccessor httpContextAccessor,
        IOptions<AuthConfig> authConfigOptions)
    : ICookiesContextService
    {
        private static readonly CookieOptions CookieOptions = new()
            { HttpOnly = true, Expires = DateTimeOffset.UtcNow.AddYears(1) };

        public void AddAccessToken(string token)
        {
            httpContextAccessor.HttpContext?.Response.Cookies
                .Append(authConfigOptions.Value.AccessTokenName, token, CookieOptions);
        }

        
        public void AddSessionToken(string token)
        {
            httpContextAccessor.HttpContext?.Response.Cookies
                .Append(authConfigOptions.Value.RefreshTokenName, token, CookieOptions);
        }

        public void DeleteAccessToken()
        {
            httpContextAccessor.HttpContext?.Response.Cookies
                .Delete(authConfigOptions.Value.AccessTokenName);
        }

        public void DeleteSessionToken()
        {
            httpContextAccessor.HttpContext?.Response.Cookies
                .Delete(authConfigOptions.Value.RefreshTokenName);
        }

        public void MarkAsNew()
        {
            CookieActionHeader cookieActionHeader = authConfigOptions.Value.Headers.CookieAction;

            httpContextAccessor.HttpContext?.Response.Headers.Append(
                cookieActionHeader.Name,
                cookieActionHeader.Values.New);
        }
    }
}
