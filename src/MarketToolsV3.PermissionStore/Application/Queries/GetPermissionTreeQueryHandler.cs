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

namespace MarketToolsV3.PermissionStore.Application.Queries;

public class GetPermissionTreeQueryHandler(
    IRepository<ModuleEntity> permissionsRepository,
    IExtensionRepository extensionRepository,
    IPermissionNodeService permissionsNodeService,
    IPermissionSettingContextService permissionSettingContextService,
    IPermissionsUtility permissionsUtility)
    : IRequestHandler<GetPermissionTreeQuery, IEnumerable<PermissionSettingViewNodeDto>>
{
    public async Task<IEnumerable<PermissionSettingViewNodeDto>> Handle(GetPermissionTreeQuery request, CancellationToken cancellationToken)
    {
        permissionSettingContextService.SetContext(request.Permissions);

        IQueryable<PermissionValueObject> pathsQuery = permissionsRepository
            .BuildPathsQueryByParentModule(request.Module);

        var permissions = await extensionRepository.ToListAsync(pathsQuery, cancellationToken);

        var segments = permissions.Select(p => p.Path.Split(':'));

        var permissionNodes = permissionsNodeService.CreateNodes(segments, 0);

        return permissionsNodeService.ConvertToViewNodes(permissionNodes);
    }

    private PermissionSettingViewNodeDto SetNames(PermissionSettingViewNodeDto node)
    {
        var newNode = permissionsUtility.SetName(node);

        return newNode with
        {
            Nodes = newNode
                .Nodes
                .Select(SetNames)
                .ToList()
        };
    }

    private PermissionSettingViewNodeDto SetStatuses(PermissionSettingViewNodeDto node)
    {
        var newNode = permissionSettingContextService.SetStatus(node);

        return newNode with
        {
            Nodes = newNode
                .Nodes
                .Select(SetStatuses)
                .ToList()
        };
    }
}