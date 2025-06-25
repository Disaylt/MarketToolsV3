using MarketToolsV3.PermissionStore.Application.Enums;

namespace MarketToolsV3.PermissionStore.Application.Models;

public record PermissionSettingViewNodeDto(string Name) : PermissionSettingViewDto(Name)
{
    public IEnumerable<PermissionSettingViewNodeDto> Nodes { get; init; } = [];
}