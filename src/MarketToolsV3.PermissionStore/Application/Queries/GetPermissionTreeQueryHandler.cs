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
    IRepository<PermissionEntity> permissionsRepository,
    IExtensionRepository extensionRepository)
    : IRequestHandler<GetPermissionTreeQuery, IReadOnlyCollection<PermissionSettingNodeDto>>
{
    public async Task<IReadOnlyCollection<PermissionSettingNodeDto>> Handle(GetPermissionTreeQuery request, CancellationToken cancellationToken)
    {
        IQueryable<string> pathsQuery = permissionsRepository.BuildPathsQueryByRangeModules(request.Modules);
        var paths = await extensionRepository.ToListAsync(pathsQuery, cancellationToken);

        return paths
            .GroupBy(mp => mp.Split(':')[0])
            .Select(group => 
                BuildPermissionHierarchy(group.Key, [.. group]))
            .ToList();
    }

    private PermissionSettingNodeDto BuildPermissionHierarchy(
        string currentSegment,
        IReadOnlyCollection<string> modulePermissions)
    {
        var setting = new PermissionSettingNodeDto
        {
            Name = currentSegment,
            View = permissionsUtility.FindOrDefaultByNameView(currentSegment)
        };

        var childPermissions = modulePermissions
            .Where(mp => mp.StartsWith(currentSegment + ":"))
            .ToList();

        if (childPermissions.Count != 0)
        {
            var nextSegmentGroups = childPermissions
                .Select(mp =>
                {
                    var segments = mp.Split(':');
                    var nextSegment = segments[Array.IndexOf(segments, currentSegment) + 1];
                    return new { NextSegment = nextSegment, ModulePermission = mp };
                })
                .GroupBy(x => x.NextSegment);

            foreach (var nextGroup in nextSegmentGroups)
            {
                var childSetting = BuildPermissionHierarchy(
                    $"{currentSegment}:{nextGroup.Key}",
                    [.. nextGroup.Select(x => x.ModulePermission)]);

                setting.Nodes.Add(childSetting);
            }
        }

        return setting;
    }
}