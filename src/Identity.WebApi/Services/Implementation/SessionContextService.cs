using Identity.WebApi.Services.Interfaces;
using MarketToolsV3.ConfigurationManager.Models;
using Microsoft.Extensions.Options;

namespace Identity.WebApi.Services.Implementation
{
    public class SessionContextService(IHttpContextAccessor httpContextAccessor,
        IOptions<AuthConfig> authConfigOptions)
    : ISessionContextService
    {
        public void MarkAsDelete(string id)
        {
            SessionRemoveHeader sessionActionHeader = authConfigOptions.Value.Headers.SessionRemove;

            httpContextAccessor.HttpContext?.Response.Headers.Append(
                sessionActionHeader.Name, id);
        }
    }
}
