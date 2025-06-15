using MarketToolsV3.PermissionStore.Application.Enums;
using MarketToolsV3.PermissionStore.Application.Models;

namespace MarketToolsV3.PermissionStore.Application.Services.Abstract;

public interface IPermissionsNodeService
{
    PermissionSettingNodeDto BuildPermissionHierarchy(
        string currentSegment,
        IReadOnlyCollection<string> modulePermissions);

    void SetStatuses(PermissionSettingNodeDto node, Dictionary<string, PermissionStatusEnum> permissionAndStatusPairs);
}