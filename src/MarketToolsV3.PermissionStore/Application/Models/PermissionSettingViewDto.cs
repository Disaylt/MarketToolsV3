using MarketToolsV3.PermissionStore.Application.Enums;

namespace MarketToolsV3.PermissionStore.Application.Models;

public record PermissionSettingViewDto(string Name) : PermissionSettingDto
{
    public string View { get; init; } = Name;
}