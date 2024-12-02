using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Identity.Application.Models;
using Identity.Application.Services;
using Identity.Domain.Entities;
using Identity.Domain.Seed;
using Identity.Infrastructure.Database;
using Identity.Infrastructure.Repositories;
using Identity.Infrastructure.Services;
using Identity.Infrastructure.Services.Claims;
using Identity.Infrastructure.Services.Tokens;
using MarketToolsV3.ConfigurationManager.Models;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Infrastructure
{
    public static class RegistrationServicesExtension
    {
        public static IServiceCollection AddMessageBroker(this IServiceCollection collection, MessageBrokerConfig messageBrokerConfig)
        {
            collection.AddMassTransit(mt =>
            {
                mt.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(messageBrokerConfig.RabbitMqConnection,
                        "/",
                        h =>
                        {
                            h.Username(messageBrokerConfig.RabbitMqLogin);
                            h.Password(messageBrokerConfig.RabbitMqPassword);
                        });

                    cfg.ConfigureEndpoints(context);
                });
            });

            return collection;
        }

        public static IServiceCollection AddInfrastructureLayer(this IServiceCollection collection, ServiceConfiguration serviceConfiguration)
        {

            collection.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            collection.AddScoped<IEventRepository, EventRepository>();
            collection.AddScoped<ISessionService, SessionService>();
            collection.AddScoped<IUnitOfWork, EfCoreUnitOfWork<IdentityDbContext>>();
            collection.AddScoped<IIdentityPersonService, IdentityPersonService>();
            collection.AddNpgsql<IdentityDbContext>(serviceConfiguration.DatabaseConnection);

            collection.AddStackExchangeRedisCache(opt =>
            {
                opt.Configuration = serviceConfiguration.RedisConnection;
            });

            collection.AddIdentityCore<IdentityPerson>(options =>
                {
                    options.User.RequireUniqueEmail = true;
                    options.Password.RequireDigit = false;
                    options.Password.RequiredLength = 6;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireLowercase = false;
                })
                .AddEntityFrameworkStores<IdentityDbContext>()
                .AddDefaultTokenProviders();

            collection.AddSingleton<IJwtSecurityTokenHandler, AppJwtSecurityTokenHandler>();
            collection.AddSingleton<IJwtTokenService, JwtTokenService>();
            collection.AddSingleton<IRolesClaimService, RolesClaimService>();
            collection.AddSingleton<IClaimsService<JwtAccessTokenDto>, JwtAccessClaimsService>();
            collection.AddSingleton<IClaimsService<JwtRefreshTokenDto>, JwtRefreshClaimsService>();
            collection.AddSingleton<ITokenService<JwtAccessTokenDto>, JwtAccessTokenService>();
            collection.AddSingleton<ITokenService<JwtRefreshTokenDto>, JwtRefreshTokenService>();
            collection.AddSingleton(typeof(ICacheRepository<>), typeof(DefaultCacheRepository<>));

            return collection;
        }
    }
}
