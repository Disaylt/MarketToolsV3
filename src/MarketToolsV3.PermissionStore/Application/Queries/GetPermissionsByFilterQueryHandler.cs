using MarketToolsV3.PermissionStore.Application.Models;
using MarketToolsV3.PermissionStore.Application.Services.Abstract;
using MarketToolsV3.PermissionStore.Domain.Entities;
using MediatR;

namespace MarketToolsV3.PermissionStore.Application.Queries;

public class GetPermissionsByFilterQueryHandler(
    IPermissionsEntityService permissionsEntityService)
    : IRequestHandler<GetPermissionsByFilterQuery, ModuleGroupDto>
{
    public async Task<ModuleGroupDto> Handle(GetPermissionsByFilterQuery request, CancellationToken cancellationToken)
    {
        IReadOnlyCollection<PermissionEntity> entities = await permissionsEntityService
            .GetRangeByModuleAsync(request.Module, cancellationToken);

        return new ModuleGroupDto
        {
            Module = request.Module,
            Permissions = MapPermissions(entities)
        };
    }

    private IReadOnlyCollection<ModulePermissionDto> MapPermissions(IEnumerable<PermissionEntity> entities)
    {
        return entities
            .Select(x => new ModulePermissionDto()
            {
                Path = x.Path,
                ViewName = x.ViewName
            })
            .ToList();
    }
}