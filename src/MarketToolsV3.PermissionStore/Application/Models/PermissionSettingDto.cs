using MarketToolsV3.PermissionStore.Application.Enums;

namespace MarketToolsV3.PermissionStore.Application.Models;

public class PermissionSettingDto
{
    public required string Path { get; init; }
    public PermissionStatusEnum Status { get; init; }
}