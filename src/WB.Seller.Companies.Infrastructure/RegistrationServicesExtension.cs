using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Dapper;
using System.Threading.Tasks;
using WB.Seller.Companies.Domain.Seed;
using WB.Seller.Companies.Infrastructure.Database;
using WB.Seller.Companies.Infrastructure.Repositories;
using WB.Seller.Companies.Infrastructure.Seed.Implementation;
using WB.Seller.Companies.Application.Models;
using WB.Seller.Companies.Application.QueryData.Companies;
using WB.Seller.Companies.Infrastructure.QueryDataHandlers.Companies;
using Npgsql;
using WB.Seller.Companies.Domain.Enums;

namespace WB.Seller.Companies.Infrastructure
{
    public static class RegistrationServicesExtension
    {
        public static IServiceCollection AddInfrastructureLayer(this IServiceCollection collection,
            ServiceConfiguration serviceConfiguration)
        {
            collection.AddNpgsql<WbSellerCompaniesDbContext>(serviceConfiguration.DatabaseConnection);
            collection.AddScoped<IDbConnection>(_ => new NpgsqlConnection(serviceConfiguration.DatabaseConnection));

            collection.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            collection.AddScoped<IUnitOfWork, EfCoreUnitOfWork<WbSellerCompaniesDbContext>>();

            collection.AddSingleton<IMapperFactory, MapperFactory>();

            collection.AddScoped<IQueryDataHandler<SlimCompanyRoleGroupsQueryData, IEnumerable<GroupDto<SubscriptionRole, CompanySlimInfoDto>>>, SlimCompanyRoleGroupsQueryDataHandler>();
            
            return collection;
        }
    }
}
