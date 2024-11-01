﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Identity.Domain.Entities;
using Identity.Domain.Seed;
using Identity.Infrastructure.Database;
using Identity.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Infrastructure
{
    public static class RegistrationServicesExtension
    {
        public static IServiceCollection AddInfrastructureLayer(this IServiceCollection collection, IConfigurationSection serviceSection)
        {
            ServiceConfiguration config = serviceSection.Get<ServiceConfiguration>()
                                          ?? throw new NullReferenceException("Users config is empty");

            collection.AddScoped<IEventRepository, EventRepository>();
            collection.AddScoped<IUnitOfWork, EfCoreUnitOfWork<IdentityDbContext>>();
            collection.AddNpgsql<IdentityDbContext>(config.Database);

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

            return collection;
        }
    }
}
