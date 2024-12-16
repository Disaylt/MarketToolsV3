using MarketToolsV3.ConfigurationManager.Abstraction;
using MarketToolsV3.ConfigurationManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WB.Seller.Api.Companies.Domain.Constant;
using WB.Seller.Api.Companies.Domain.Seed;
using WB.Seller.Api.Companies.Infrastructure.Database;

namespace MarketToolsV3.MigrationService.ContextRegistration
{
    public static class WbSellerApiCompaniesRegistrationExtension
    {
        public static void AddWbSellerApiCompaniesContext(this HostApplicationBuilder builder)
        {
            ConfigurationServiceFactory configurationServiceFactory = new(builder.Configuration);

            ITypingConfigManager<ServiceConfiguration> serviceConfigManager = configurationServiceFactory
                .CreateFromService<ServiceConfiguration>(ServiceInformation.Name);

            builder.Services.AddNpgsql<ApiCompaniesDbContext>(serviceConfigManager.Value.DatabaseConnection);
        }
    }
}
