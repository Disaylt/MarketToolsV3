using MarketToolsV3.PermissionStore.Application.Models;
using MarketToolsV3.PermissionStore.Domain.ValueObjects;

namespace MarketToolsV3.PermissionStore.Application.Utilities.Abstract;

public interface IPermissionSettingNodeBuilder
{
    IPermissionSettingNodeBuilder SetNodes(IEnumerable<PermissionNodeDto> nodes);
    IPermissionSettingNodeBuilder SetViewNames(IPermissionsUtility permissionsUtility);
    IPermissionSettingNodeBuilder SetRequireUseStatuses(IEnumerable<PermissionValueObject> permissions);
    IPermissionSettingNodeBuilder SetStatuses(IEnumerable<PermissionSettingDto> permissions);
    IEnumerable<PermissionSettingViewNodeDto> Build();
}