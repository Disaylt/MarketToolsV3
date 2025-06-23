using MarketToolsV3.PermissionStore.Application.Enums;
using MarketToolsV3.PermissionStore.Application.Models;
using MarketToolsV3.PermissionStore.Application.Services.Abstract;
using MarketToolsV3.PermissionStore.Domain.Entities;
using MarketToolsV3.PermissionStore.Domain.Seed;
using MediatR;
using System.Collections.Generic;
using MarketToolsV3.PermissionStore.Application.Utilities.Abstract;
using MarketToolsV3.PermissionStore.Application.Extensions;

namespace MarketToolsV3.PermissionStore.Application.Queries;

public class GetPermissionTreeQueryHandler(
    IRepository<ModuleEntity> permissionsRepository,
    IExtensionRepository extensionRepository,
    IPermissionsNodeService permissionsNodeService)
    : IRequestHandler<GetPermissionTreeQuery, IReadOnlyCollection<PermissionSettingViewNodeDto>>
{
    public async Task<IReadOnlyCollection<PermissionSettingViewNodeDto>> Handle(GetPermissionTreeQuery request, CancellationToken cancellationToken)
    {
        IQueryable<string> pathsQuery = permissionsRepository.BuildPathsQueryByParentModule(request.Module);
        var paths = await extensionRepository.ToListAsync(pathsQuery, cancellationToken);


        return [.. paths
            .GroupBy(mp => mp.Split(':')[0])
            .Select(group =>
            {
                var setting = permissionsNodeService.BuildPermissionHierarchy(group.Key, [.. group]);
                permissionsNodeService.SetStatuses(setting, permissionAndStatusPairs);

                return setting;
            })];
    }
}