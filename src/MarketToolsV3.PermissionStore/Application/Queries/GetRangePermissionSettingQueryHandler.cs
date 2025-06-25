using MarketToolsV3.PermissionStore.Application.Extensions;
using MarketToolsV3.PermissionStore.Application.Models;
using MarketToolsV3.PermissionStore.Application.Services.Abstract;
using MarketToolsV3.PermissionStore.Application.Services.Implementation;
using MarketToolsV3.PermissionStore.Application.Utilities.Abstract;
using MarketToolsV3.PermissionStore.Domain.Entities;
using MarketToolsV3.PermissionStore.Domain.Seed;
using MarketToolsV3.PermissionStore.Domain.ValueObjects;
using MediatR;

namespace MarketToolsV3.PermissionStore.Application.Queries;

public class GetRangePermissionSettingQueryHandler(
    IRepository<ModuleEntity> permissionsRepository,
    IExtensionRepository extensionRepository,
    IPermissionSettingContextService permissionSettingContextService)
    : IRequestHandler<GetRangePermissionSettingQuery, IEnumerable<PermissionSettingDto>>
{
    public async Task<IEnumerable<PermissionSettingDto>> Handle(GetRangePermissionSettingQuery request, CancellationToken cancellationToken)
    {
        permissionSettingContextService.SetContext(request.Permissions);

        IQueryable<PermissionValueObject> pathsQuery = permissionsRepository
            .BuildPathsQueryByParentModule(request.Module);

        var permissions = await extensionRepository.ToListAsync(pathsQuery, cancellationToken);

        return permissions
            .Select(p => new PermissionSettingDto
            {
                Path = p.Path,
                RequireUse = p.RequireUse
            })
            .Select(permissionSettingContextService.SetStatus);
    }
}