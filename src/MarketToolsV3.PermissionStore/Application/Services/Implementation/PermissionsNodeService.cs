using MarketToolsV3.PermissionStore.Application.Models;
using MarketToolsV3.PermissionStore.Application.Utilities.Abstract;
using MarketToolsV3.PermissionStore.Application.Utilities.Implementation;
using MongoDB.Driver;

namespace MarketToolsV3.PermissionStore.Application.Services.Implementation;

public class PermissionsNodeService(
    IPermissionsUtility permissionsUtility,
    IPermissionsNodeUtility permissionsNodeUtility)
{
    public PermissionSettingNodeDto BuildPermissionHierarchy(string currentSegment,
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

    private IReadOnlyList<PermissionSettingNodeDto> AddNextSegments(IDictionary<string, IEnumerable<PermissionSlimNodeDto>> childPermissions, string currentSegment)
    {
        List<PermissionSettingNodeDto> nodes = [];
        foreach (var nextGroup in childPermissions)
        {
            var childSetting = BuildPermissionHierarchy(
                $"{currentSegment}:{nextGroup.Key}",
                [.. nextGroup.Value.Select(x => x.Permission)]);

            nodes.Add(childSetting);
        }

        return nodes;
    }

    private PermissionSettingNodeDto CreateNode(string segment)
    {
        return new PermissionSettingNodeDto
        {
            Name = segment,
            View = permissionsUtility.FindOrDefaultByPathView(segment)
        };
    }
}