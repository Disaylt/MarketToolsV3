using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WB.Seller.Companies.Domain.Seed;
using WB.Seller.Companies.Infrastructure.Database;

namespace WB.Seller.Companies.Infrastructure
{
    public static class RegistrationServicesExtension
    {
        public static IServiceCollection AddInfrastructureLayer(this IServiceCollection collection,
            ServiceConfiguration serviceConfiguration)
        {
            collection.AddNpgsql<ApiCompaniesDbContext>(serviceConfiguration.DatabaseConnection);

            return collection;
        }
    }
}
