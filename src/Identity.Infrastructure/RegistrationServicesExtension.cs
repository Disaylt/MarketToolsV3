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
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Infrastructure
{
    public static class RegistrationServicesExtension
    {
        public static IServiceCollection AddInfrastructureLayer(this IServiceCollection collection, GlobalConfiguration<ServiceConfiguration> configuration)
        {

            collection.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            collection.AddScoped<IEventRepository, EventRepository>();
            collection.AddScoped<ISessionService, SessionService>();
            collection.AddScoped<IUnitOfWork, EfCoreUnitOfWork<IdentityDbContext>>();
            collection.AddScoped<IIdentityPersonService, IdentityPersonService>();
            collection.AddNpgsql<IdentityDbContext>(configuration.Service.Database);

            collection.AddStackExchangeRedisCache(opt =>
            {
                opt.Configuration = configuration.Service.Redis;
            });

            collection.AddMassTransit(mt =>
            {
                mt.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(configuration.General.MessageBrokerRabbitMqConnection,
                        "/", 
                        h =>
                        {
                            h.Username(configuration.General.MessageBrokerRabbitMqLogin);
                            h.Password(configuration.General.MessageBrokerRabbitMqPassword);
                        });

                    cfg.ConfigureEndpoints(context);
                });
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
