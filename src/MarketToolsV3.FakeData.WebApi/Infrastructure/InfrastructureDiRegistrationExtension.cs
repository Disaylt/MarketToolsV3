using MarketToolsV3.FakeData.WebApi.Application.Services.Abstract;
using MarketToolsV3.FakeData.WebApi.Domain.Seed;
using MarketToolsV3.FakeData.WebApi.Infrastructure.Database;
using MarketToolsV3.FakeData.WebApi.Infrastructure.Repositories.Abstract;
using MarketToolsV3.FakeData.WebApi.Infrastructure.Repositories.Implementation;
using MarketToolsV3.FakeData.WebApi.Infrastructure.Services.Abstract;
using MarketToolsV3.FakeData.WebApi.Infrastructure.Services.Implementation;
using MarketToolsV3.FakeData.WebApi.Infrastructure.Utilities.Abstract;
using MarketToolsV3.FakeData.WebApi.Infrastructure.Utilities.Implementation;
using Microsoft.Extensions.DependencyInjection;

namespace MarketToolsV3.FakeData.WebApi.Infrastructure
{
    public static class InfrastructureDiRegistrationExtension
    {
        public static IServiceCollection AddInfrastructureService(this IServiceCollection serviceCollection, ServiceConfig serviceConfig)
        {
            serviceCollection.AddNpgsql<FakeDataDbContext>(serviceConfig.DatabaseConnection);

            serviceCollection.AddScoped<IFakeDataTaskEntityService, FakeDataTaskEntityService>();
            serviceCollection.AddScoped<ITaskDetailsEntityService, TaskDetailsEntityService>();
            serviceCollection.AddScoped<ITaskDetailsHandleFacadeService, TaskDetailsHandleFacadeService>();
            serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();
            serviceCollection.AddScoped<ICookieEntityService, CookieEntityService>();
            serviceCollection.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            serviceCollection.AddScoped(typeof(IQueryableRepository<>), typeof(QueryableRepository<>));

            serviceCollection.AddSingleton<IMapperFactory, MapperFactory>();
            serviceCollection.AddSingleton<ICookieContainerBackgroundService, CookieContainerBackgroundService>();


            serviceCollection.AddSingleton<ITaskHttpClientFactory, TaskHttpClientFactory>();
            serviceCollection.AddSingleton<ITaskHttpLockStore, TaskHttpLockStore>();

            return serviceCollection;
        }
    }
}
