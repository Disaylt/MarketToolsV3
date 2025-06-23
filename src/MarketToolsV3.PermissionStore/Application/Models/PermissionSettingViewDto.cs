using MarketToolsV3.PermissionStore.Application.Enums;

namespace MarketToolsV3.PermissionStore.Application.Models;

public record PermissionSettingViewDto : PermissionSettingDto
{
    public required string Name { get; init; }
}