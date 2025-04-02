using Identity.Application.Models;
using Identity.Application.Services;
using Identity.WebApi.Services;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using Identity.WebApi.Models;
using Identity.WebApi.Services.Interfaces;
using Microsoft.Extensions.Primitives;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Identity.WebApi.Middlewares
{
    public class AuthDetailsMiddleware(RequestDelegate next)
    {
        public async Task Invoke(HttpContext httpContext,
            IAuthContext authContext)
        {
            Claim? sessionIdClaim = httpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid);
            if (sessionIdClaim != null)
            {
                authContext.SessionId = sessionIdClaim.Value;
            }

            authContext.UserId = httpContext.User?.FindFirstValue(ClaimTypes.NameIdentifier);

            await next(httpContext);
        }
    }
}
