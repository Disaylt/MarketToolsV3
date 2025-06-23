using MarketToolsV3.PermissionStore.Application.Models;
using MarketToolsV3.PermissionStore.Domain.Entities;
using MarketToolsV3.PermissionStore.Domain.Seed;
using MarketToolsV3.PermissionStore.Domain.ValueObjects;
using MediatR;

namespace MarketToolsV3.PermissionStore.Application.Extensions;

public static class PermissionRepositoryExtensions
{
    public static IQueryable<PermissionValueObject> BuildPathsQueryByParentModule(
        this IRepository<ModuleEntity> modulesRepository,
        string module)
    {
        return modulesRepository
            .AsQueryable()
            .Where(x=> x.Permissions.Any(p => p.AvailableModules.Contains(module)))
            .SelectMany(x => x.Permissions);
    }
}