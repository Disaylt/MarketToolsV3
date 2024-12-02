using Identity.Domain.Seed;
using Identity.WebApi.Services;
using MarketToolsV3.ConfigurationManager.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Identity.WebApi
{
    public static class ServiceRegistrationExtension
    {
        public static void AddServiceAuthentication(this IServiceCollection collection, AuthConfig authConfig)
        {
            collection.AddScoped<IAuthContext, AuthContext>();

            byte[] secretBytes = Encoding.UTF8.GetBytes(authConfig.AuthSecret);

            collection.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt =>
                {
                    {
                        opt.IncludeErrorDetails = false;
                        opt.SaveToken = true;
                        opt.RequireHttpsMetadata = false;
                        opt.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = authConfig.IsCheckValidIssuer,
                            ValidateAudience = authConfig.IsCheckValidAudience,
                            ValidateLifetime = true,
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
