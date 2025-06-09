using MarketToolsV3.PermissionStore.Application.Models;
using MarketToolsV3.PermissionStore.Domain.Entities;
using MarketToolsV3.PermissionStore.Domain.Seed;
using MediatR;

namespace MarketToolsV3.PermissionStore.Application.Extensions;

public static class PermissionRepositoryExtensions
{
    public static IQueryable<string> BuildPathsQueryByRangeModules(
        this IRepository<PermissionEntity> permissionsRepository,
        IEnumerable<string> modules)
    {
        return permissionsRepository
            .AsQueryable()
            .Where(x=> modules.Contains(x.Module))
            .Select(x => x.Path);
    }
}