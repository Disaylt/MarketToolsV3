using MarketToolsV3.PermissionStore.Application.Enums;
using MarketToolsV3.PermissionStore.Application.Models;
using MarketToolsV3.PermissionStore.Application.Services.Abstract;
using MarketToolsV3.PermissionStore.Domain.Entities;
using MarketToolsV3.PermissionStore.Domain.Seed;
using MediatR;
using System.Collections.Generic;
using MarketToolsV3.PermissionStore.Application.Utilities.Abstract;
using MarketToolsV3.PermissionStore.Application.Extensions;
using MarketToolsV3.PermissionStore.Domain.ValueObjects;
using MarketToolsV3.PermissionStore.Application.Services.Implementation;
using System.Threading;

namespace MarketToolsV3.PermissionStore.Application.Queries;

public class GetPermissionTreeQueryHandler(
    IRepository<ModuleEntity> permissionsRepository,
    IExtensionRepository extensionRepository,
    IPermissionNodeService permissionsNodeService,
    IPermissionSettingNodeBuilder permissionSettingNodeBuilder,
    IPermissionsUtility permissionsUtility)
    : IRequestHandler<GetPermissionTreeQuery, IEnumerable<PermissionSettingViewNodeDto>>
{
    public async Task<IEnumerable<PermissionSettingViewNodeDto>> Handle(GetPermissionTreeQuery request, CancellationToken cancellationToken)
    {
        var permissions = await GetPermissionsAsync(request.Module, cancellationToken);
        var permissionNodes = CreateNodes(permissions);

        return permissionSettingNodeBuilder
            .SetNodes(permissionNodes)
            .SetStatuses(request.Permissions)
            .SetRequireUseStatuses(permissions)
            .SetViewNames(permissionsUtility)
            .Build();
    }

    private IEnumerable<PermissionNodeDto> CreateNodes(IEnumerable<PermissionValueObject> permissions)
    {
        var segments = permissions.Select(p => p.Path.Split(':'));

        return permissionsNodeService.CreateNodes(segments, 0);
    }

    private async Task<IReadOnlyCollection<PermissionValueObject>> GetPermissionsAsync(string module, CancellationToken cancellationToken)
    {
        IQueryable<PermissionValueObject> pathsQuery = permissionsRepository
            .BuildPathsQueryByParentModule(module);

        return await extensionRepository.ToListAsync(pathsQuery, cancellationToken);
    }
}