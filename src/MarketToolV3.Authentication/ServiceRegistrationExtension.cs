using MarketToolsV3.ConfigurationManager.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketToolV3.Authentication.Services.Abstract;
using MarketToolV3.Authentication.Services.Implementation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace MarketToolV3.Authentication
{
    public static class ServiceRegistrationExtension
    {
        public static void AddServiceAuthentication(this IServiceCollection collection, AuthConfig authConfig, bool validateLifetime = true)
        {
            collection.AddScoped<IAuthContext, AuthContext>();

            byte[] secretBytes = Encoding.UTF8.GetBytes(authConfig.AuthSecret);

            collection.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt =>
                {
                    {
                        opt.IncludeErrorDetails = false;
                        opt.SaveToken = true;
                        opt.RequireHttpsMetadata = true;
                        opt.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = authConfig.IsCheckValidIssuer,
                            ValidateAudience = authConfig.IsCheckValidAudience,
                            ValidateLifetime = validateLifetime,
                            ValidateIssuerSigningKey = true,
                            ValidAudience = authConfig.ValidAudience,
                            ValidIssuer = authConfig.ValidIssuer,
                            IssuerSigningKey = new SymmetricSecurityKey(secretBytes)
                        };
                    }
                });
        }
    }
}
