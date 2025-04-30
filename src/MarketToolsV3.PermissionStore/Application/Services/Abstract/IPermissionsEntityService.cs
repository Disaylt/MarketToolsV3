using MarketToolsV3.PermissionStore.Domain.Entities;

namespace MarketToolsV3.PermissionStore.Application.Services.Abstract;

public interface IPermissionsEntityService
{
    Task<List<PermissionEntity>> GetRangeByModuleAsync(string module, CancellationToken ct);
}