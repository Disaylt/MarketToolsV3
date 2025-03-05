using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Identity.Application.Models;
using Identity.Application.QueryObjects;
using Identity.Application.Services.Abstract;
using Identity.Domain.Entities;
using Identity.Domain.Seed;
using Identity.Infrastructure.Database;
using Identity.Infrastructure.QueryableHandlers;
using Identity.Infrastructure.QueryObjectAdapters;
using Identity.Infrastructure.Repositories;
using Identity.Infrastructure.Services.Abstract;
using Identity.Infrastructure.Services.Abstract.Claims;
using Identity.Infrastructure.Services.Abstract.Tokens;
using Identity.Infrastructure.Services.Implementation;
using Identity.Infrastructure.Services.Implementation.Claims;
using Identity.Infrastructure.Services.Implementation.Tokens;
using MarketToolsV3.ConfigurationManager.Models;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace Identity.Infrastructure
{
    public static class RegistrationServicesExtension
    {
        public static IServiceCollection AddInfrastructureLayer(this IServiceCollection collection, ServiceConfiguration serviceConfiguration)
        {
            collection.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            collection.AddScoped<IEventRepository, EventRepository>();
            collection.AddScoped<ISessionService, SessionService>();
            collection.AddScoped<IUnitOfWork, EfCoreUnitOfWork<IdentityDbContext>>();
            collection.AddScoped<IIdentityPersonService, IdentityPersonService>();
            collection.AddScoped<IExtensionRepository, ExtensionRepository>();
            collection.AddScoped<IQueryableHandler<Session, SessionDto>, SessionToTransferMapQueryableHandler>();
            collection.AddScoped<IQueryObjectHandler<GetActivateSessionQueryObject, Session>, GetActivateSessionQueryObjectHandler>();
            collection.AddNpgsql<IdentityDbContext>(serviceConfiguration.DatabaseConnection);

            AddRedisCache(collection, serviceConfiguration.RedisConfig);

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

        private static void AddRedisCache(IServiceCollection collection, RedisConfig redisConfig)
        {
            if (string.IsNullOrEmpty(redisConfig.Host))
            {
                throw new NullReferenceException("Redis host is null.");
            }

            collection.AddStackExchangeRedisCache(opt =>
            {
                opt.ConfigurationOptions = new ConfigurationOptions
                {
                    EndPoints =
                    {
                        { redisConfig.Host, redisConfig.Port }
                    },
                    User = redisConfig.User,
                    Password = redisConfig.Password,
                    DefaultDatabase = redisConfig.Database
                };
            });
        }
    }
}
