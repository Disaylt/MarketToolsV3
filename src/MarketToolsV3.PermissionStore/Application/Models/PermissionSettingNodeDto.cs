using MarketToolsV3.PermissionStore.Application.Enums;

namespace MarketToolsV3.PermissionStore.Application.Models;

public record PermissionSettingNodeDto
{
    public required string Name { get; init; }
    public required string View { get; init; }
    public PermissionStatusEnum Status { get; init; }
    public IReadOnlyList<PermissionSettingNodeDto> Nodes { get; init; } = [];
}