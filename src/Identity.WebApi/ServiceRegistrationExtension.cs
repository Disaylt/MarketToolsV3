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
        public static void AddWebApiServices(this IServiceCollection collection, GlobalConfiguration<ServiceConfiguration> configuration)
        {
            collection.AddScoped<IAuthContext, AuthContext>();

            byte[] secretBytes = Encoding.UTF8.GetBytes(configuration.General.AuthSecret);

            collection.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt =>
                {
                    {
                        opt.IncludeErrorDetails = false;
                        opt.SaveToken = true;
                        opt.RequireHttpsMetadata = false;
                        opt.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = false,
                            ValidateAudience = false,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            ValidAudience = configuration.Service.ValidAudience,
                            ValidIssuer = configuration.Service.ValidIssuer,
                            IssuerSigningKey = new SymmetricSecurityKey(secretBytes)
                        };
                    }
                });
        }
    }
}
