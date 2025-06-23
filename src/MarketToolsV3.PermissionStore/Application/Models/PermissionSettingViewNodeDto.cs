using MarketToolsV3.PermissionStore.Application.Enums;

namespace MarketToolsV3.PermissionStore.Application.Models;

public record PermissionSettingViewNodeDto : PermissionSettingViewDto
{
    public IReadOnlyList<PermissionSettingViewNodeDto> Nodes { get; init; } = [];
}