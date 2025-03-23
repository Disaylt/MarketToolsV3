using MarketToolsV3.ApiGateway.Middlewares;
using MarketToolsV3.ApiGateway.Models.Tokens;
using MarketToolsV3.ApiGateway.Services.Implementation;
using MarketToolsV3.ApiGateway.Services.Interfaces;
using MarketToolsV3.ConfigurationManager.Models;
using Proto.Contract.Identity;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace MarketToolsV3.ApiGateway;

public static class ServiceRegistrationExtension
{
    public static string AddDevCorsServices(this IServiceCollection services)
    {
        string name = "devAllowSpecificOrigins";

        services.AddCors(options =>
        {
            options.AddPolicy(name: name,
                policy =>
                {
                    policy.WithOrigins("http://localhost:5173")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
        });

        return name;
    }

    public static IServiceCollection AddServiceAuthentication(this IServiceCollection collection, AuthConfig authConfig)
    {
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
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidAudience = authConfig.ValidAudience,
                        ValidIssuer = authConfig.ValidIssuer,
                        IssuerSigningKey = new SymmetricSecurityKey(secretBytes)
                    };
                }
            });

        return collection;
    }

    public static IServiceCollection AddApiGatewayServices(this IServiceCollection collection)
    {
        collection.AddScoped<IAuthContext, AuthContext>();

        collection.AddSingleton<ITokenReader<AccessToken>, AccessTokenReader>();
        collection.AddSingleton<ITokenReader<SessionToken>, SessionTokenReader>();
        collection.AddSingleton<IAccessTokenService, AccessTokenService>();
        collection.AddSingleton<IJwtSecurityTokenHandler, AppJwtSecurityTokenHandler>();

        collection.AddDistributedMemoryCache();
        collection.AddSingleton(typeof(ICacheRepository<>), typeof(DefaultCacheRepository<>));

        return collection;
    }

    public static IServiceCollection AddAuthGrpcClient(this IServiceCollection collection,
        ServicesAddressesConfig servicesAddressesConfig)
    {
        string? identityGrpcAddress = servicesAddressesConfig.Identity.Grpc;

        if (string.IsNullOrEmpty(identityGrpcAddress) == false)
        {
            collection.AddGrpcClient<Auth.AuthClient>(c =>
            {
                c.Address = new Uri(identityGrpcAddress);
            });

            collection.AddGrpcClient<Session.SessionClient>(c =>
            {
                c.Address = new Uri(identityGrpcAddress);
            });
        }

        return collection;
    }
}