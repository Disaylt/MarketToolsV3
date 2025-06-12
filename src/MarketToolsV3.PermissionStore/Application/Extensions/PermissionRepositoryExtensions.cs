using MarketToolsV3.PermissionStore.Application.Models;
using MarketToolsV3.PermissionStore.Domain.Entities;
using MarketToolsV3.PermissionStore.Domain.Seed;
using MediatR;

namespace MarketToolsV3.PermissionStore.Application.Extensions;

public static class PermissionRepositoryExtensions
{
    public static IQueryable<string> BuildPathsQueryByParentModule(
        this IRepository<ModuleEntity> modulesRepository,
        string module)
    {
        return modulesRepository
            .AsQueryable()
            .Where(x=> x.ParentModules.Contains(module))
            .SelectMany(x => x.Permissions);
    }
}