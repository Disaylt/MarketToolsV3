using MarketToolsV3.ConfigurationManager.Abstraction;
using MarketToolsV3.ConfigurationManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Identity.Domain.Constant;
using Identity.Domain.Entities;
using Identity.Domain.Seed;
using Identity.Infrastructure.Database;
using Microsoft.AspNetCore.Identity;

namespace MarketToolsV3.MigrationService.Registrations
{
    internal static class IdentityRegistrationExtension
    {
        public static void AddIdentityContext(this HostApplicationBuilder builder)
        {
            ConfigurationServiceFactory configurationServiceFactory = new(builder.Configuration);

            ITypingConfigManager<ServiceConfiguration> serviceConfigManager = configurationServiceFactory
                .CreateFromService<ServiceConfiguration>(ServiceInformation.Name);
            builder.Services.AddNpgsql<IdentityDbContext>(serviceConfigManager.Value.DatabaseConnection);

            builder.Services.AddIdentityCore<IdentityPerson>(options =>
                {
                    options.User.RequireUniqueEmail = true;
                    options.Password.RequireDigit = false;
                    options.Password.RequiredLength = 6;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireLowercase = false;
                })
                .AddEntityFrameworkStores<IdentityDbContext>();
        }
    }
}
