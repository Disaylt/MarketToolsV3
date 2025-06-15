using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using MarketToolV3.Authentication.Services.Abstract;

namespace MarketToolV3.Authentication.Middlewares
{
    public class AuthContextMiddleware(RequestDelegate next)
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
