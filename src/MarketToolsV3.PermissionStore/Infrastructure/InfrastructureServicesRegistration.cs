using MarketToolsV3.PermissionStore.Application.Services.Abstract;
using MarketToolsV3.PermissionStore.Domain.Entities;
using MarketToolsV3.PermissionStore.Domain.Seed;
using MarketToolsV3.PermissionStore.Infrastructure.Database;
using MarketToolsV3.PermissionStore.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace MarketToolsV3.PermissionStore.Infrastructure;

public static class InfrastructureServicesRegistration
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        ServiceConfiguration config)
    {
        services.AddSingleton<IMongoClient>(new MongoClient(config.DatabaseConnection));
        services.AddSingleton(x =>
            x.GetRequiredService<IMongoClient>().GetDatabase(config.DatabaseName));
        services.AddSingleton(x =>
            x.GetRequiredService<IMongoDatabase>().GetCollection<ModuleEntity>(config.PermissionsCollectionName));

        services.AddScoped<ITransactionContext, MongoTransactionContext>();
        services.AddScoped<IClientSessionHandleContext, ClientSessionHandleContext>();
        services.AddScoped<IExtensionRepository, MongoExtensionRepository>();
        services.AddScoped<IRepository<ModuleEntity>, PermissionsMongoRepository>();

        return services;
    }
}