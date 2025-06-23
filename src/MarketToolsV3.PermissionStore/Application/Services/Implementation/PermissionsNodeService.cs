using MarketToolsV3.PermissionStore.Application.Enums;
using MarketToolsV3.PermissionStore.Application.Models;
using MarketToolsV3.PermissionStore.Application.Services.Abstract;
using MarketToolsV3.PermissionStore.Application.Utilities.Abstract;
using MarketToolsV3.PermissionStore.Application.Utilities.Implementation;
using MongoDB.Driver;

namespace MarketToolsV3.PermissionStore.Application.Services.Implementation;

public class PermissionsNodeService(
    IPermissionsUtility permissionsUtility,
    IPermissionsNodeUtility permissionsNodeUtility)
: IPermissionsNodeService
{
    public PermissionSettingViewNodeDto BuildPermissionHierarchy(string currentSegment,
        IReadOnlyCollection<string> modulePermissions)
    {
        var setting = CreateNode(currentSegment);

        var childPermissions = permissionsNodeUtility.GetChildPermissions(modulePermissions, currentSegment);

        if (childPermissions.Count == 0) return setting;

        setting = setting with
        {
            Nodes = AddNextSegments(childPermissions, currentSegment)
        };

        return setting;
    }

    private IReadOnlyList<PermissionSettingViewNodeDto> AddNextSegments(IDictionary<string, IEnumerable<PermissionSlimNodeDto>> childPermissions, string currentSegment)
    {
        List<PermissionSettingViewNodeDto> nodes = [];
        foreach (var nextGroup in childPermissions)
        {
            var childSetting = BuildPermissionHierarchy(
                $"{currentSegment}:{nextGroup.Key}",
                [.. nextGroup.Value.Select(x => x.Permission)]);

            nodes.Add(childSetting);
        }

        return nodes;
    }

    private PermissionSettingViewNodeDto CreateNode(string segment)
    {
        return new PermissionSettingViewNodeDto
        {
            Path = segment,
            Name = permissionsUtility.FindOrDefaultByPathView(segment)
        };
    }
}