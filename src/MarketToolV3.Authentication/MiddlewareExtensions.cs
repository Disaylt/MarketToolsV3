using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketToolV3.Authentication.Middlewares;

namespace MarketToolV3.Authentication
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseAuthContext(this IApplicationBuilder app)
        {
            return app.UseMiddleware<AuthContextMiddleware>();
        }
    }
}
