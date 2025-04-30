using MarketToolsV3.PermissionStore.Application.Services.Abstract;
using MarketToolsV3.PermissionStore.Domain.Entities;
using MarketToolsV3.PermissionStore.Domain.Seed;
using MediatR;
using System.Threading;

namespace MarketToolsV3.PermissionStore.Infrastructure.Services.Implementation;

public class PermissionsEntityService(
    IRepository<PermissionEntity> permissionsRepository,
    IExtensionRepository extensionRepository)
    : IPermissionsEntityService
{
    public async Task<List<PermissionEntity>> GetRangeByModuleAsync(string module, CancellationToken ct)
    {
        IQueryable<PermissionEntity> existsPermissionsQuery = permissionsRepository
            .AsQueryable()
            .Where(x => x.Module == module);

        return await extensionRepository.ToListAsync(existsPermissionsQuery, ct);
    }
}