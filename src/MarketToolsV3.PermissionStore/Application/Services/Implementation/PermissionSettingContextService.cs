using MarketToolsV3.PermissionStore.Application.Enums;
using MarketToolsV3.PermissionStore.Application.Models;
using MarketToolsV3.PermissionStore.Application.Services.Abstract;

namespace MarketToolsV3.PermissionStore.Application.Services.Implementation;

public class PermissionSettingContextService : IPermissionSettingContextService
{
    private Dictionary<string, PermissionStatusEnum> _pathAndStatusPairs = [];
    public void SetContext(IEnumerable<PermissionSettingDto> permissions)
    {
        _pathAndStatusPairs = permissions
            .ToDictionary(x => x.Path, x => x.Status);
    } 

    public T SetStatus<T>(T permissionSetting) where T : PermissionSettingDto
    {
        return permissionSetting with
        {
            Status = _pathAndStatusPairs.GetValueOrDefault(permissionSetting.Path)
        };
    }
}