using MarketToolsV3.FakeData.WebApi.Domain.Seed;
using MarketToolsV3.FakeData.WebApi.Infrastructure.Database;
using Microsoft.Extensions.DependencyInjection;

namespace MarketToolsV3.FakeData.WebApi.Infrastructure
{
    public static class InfrastructureDiRegistrationExtension
    {
        public static IServiceCollection AddInfrastructureService(this IServiceCollection serviceCollection, ServiceConfig serviceConfig)
        {
            serviceCollection.AddNpgsql<FakeDataDbContext>(serviceConfig.DatabaseConnection);

            return serviceCollection;
        }
    }
}
