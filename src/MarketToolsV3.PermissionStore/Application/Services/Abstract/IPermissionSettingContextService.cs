using MarketToolsV3.PermissionStore.Application.Models;

namespace MarketToolsV3.PermissionStore.Application.Services.Abstract;

public interface IPermissionSettingContextService
{
    T SetStatus<T>(T permissionSetting) where T : PermissionSettingDto;
    void SetContext(IEnumerable<PermissionSettingDto> permissions);
}