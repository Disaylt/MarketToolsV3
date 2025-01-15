using Identity.WebApi.Services.Interfaces;
using MarketToolsV3.ConfigurationManager.Models;
using Microsoft.Extensions.Options;

namespace Identity.WebApi.Services.Implementation
{
    public class SessionContextService(IHttpContextAccessor httpContextAccessor,
        IOptions<AuthConfig> authConfigOptions)
    : ISessionContextService
    {
        public void MarkAsDelete()
        {
            SessionActionHeader sessionActionHeader = authConfigOptions.Value.Headers.SessionAction;

            httpContextAccessor.HttpContext?.Response.Headers.Append(
                sessionActionHeader.Name,
                sessionActionHeader.Values.Delete);
        }
    }
}
