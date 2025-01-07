using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WB.Seller.Api.Companies.Domain.Seed;
using WB.Seller.Api.Companies.Infrastructure.Database;

namespace WB.Seller.Api.Companies.Infrastructure
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
