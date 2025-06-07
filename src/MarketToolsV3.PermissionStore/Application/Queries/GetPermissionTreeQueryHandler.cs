using MarketToolsV3.PermissionStore.Application.Enums;
using MarketToolsV3.PermissionStore.Application.Models;
using MarketToolsV3.PermissionStore.Application.Services.Abstract;
using MarketToolsV3.PermissionStore.Domain.Entities;
using MarketToolsV3.PermissionStore.Domain.Seed;
using MediatR;
using System.Collections.Generic;
using MarketToolsV3.PermissionStore.Application.Utilities.Abstract;

namespace MarketToolsV3.PermissionStore.Application.Queries;

public class GetPermissionTreeQueryHandler(
    IRepository<PermissionEntity> permissionsRepository,
    IExtensionRepository extensionRepository,
    IPermissionsUtility permissionsUtility)
    : IRequestHandler<GetPermissionTreeQuery, IReadOnlyCollection<PermissionSettingNodeDto>>
{
    public async Task<IReadOnlyCollection<PermissionSettingNodeDto>> Handle(GetPermissionTreeQuery request, CancellationToken cancellationToken)
    {
        IReadOnlyCollection<string> permissions = await GetPermissionsAsync(cancellationToken);

        var pathAndStatusPairs = request.Permissions
            .ToDictionary(x => x.Path, x => x.Status);

        List<PermissionSettingNodeDto> root = [];

        var groupedByFirstSegment = permissions
            .GroupBy(mp => mp.Split(':')[0]);

        foreach (var group in groupedByFirstSegment)
        {
            var rootSetting = BuildPermissionHierarchy(group.Key, [.. group], pathAndStatusPairs);
            root.Add(rootSetting);
        }

        return root;
    }

    private PermissionSettingNodeDto BuildPermissionHierarchy(
        string currentSegment,
        IReadOnlyCollection<string> modulePermissions,
        Dictionary<string, PermissionStatusEnum> groupedByFirstSegment)
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
                    [.. nextGroup.Select(x => x.ModulePermission)],
                    groupedByFirstSegment);

                setting.Nodes.Add(childSetting);
            }
        }

        if (groupedByFirstSegment.TryGetValue(setting.Name, out var status))
        {
            setting = setting with { Status = status };
        }

        return setting;
    }

    private async Task<IReadOnlyCollection<string>> GetPermissionsAsync(CancellationToken cancellationToken)
    {
        IQueryable<string> existsPermissionsQuery = permissionsRepository
            .AsQueryable()
            .Select(x => x.Path);

        return await extensionRepository.ToListAsync(existsPermissionsQuery, cancellationToken);
    }
}