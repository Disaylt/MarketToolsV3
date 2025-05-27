using MarketToolsV3.PermissionStore.Application.Models;
using MarketToolsV3.PermissionStore.Application.Services.Abstract;
using MarketToolsV3.PermissionStore.Domain.Entities;
using MediatR;

namespace MarketToolsV3.PermissionStore.Application.Queries;

public class GetPermissionsByFilterQueryHandler(
    IPermissionsEntityService permissionsEntityService)
    : IRequestHandler<GetPermissionsByFilterQuery, IEnumerable<ModulePermissionDto>>
{
    public async Task<IEnumerable<ModulePermissionDto>> Handle(GetPermissionsByFilterQuery request, CancellationToken cancellationToken)
    {
        IReadOnlyCollection<PermissionEntity> entities = await permissionsEntityService
            .GetRangeByModuleAsync(request.Module, cancellationToken);

        return MapPermissions(entities);
    }

    private IEnumerable<ModulePermissionDto> MapPermissions(IEnumerable<PermissionEntity> entities)
    {
        return entities
            .Select(x => new ModulePermissionDto()
            {
                Path = x.Path,
                ViewName = x.ViewName
            });
    }
}