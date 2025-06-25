namespace MarketToolsV3.PermissionStore.Application.Models;

public record PermissionNodeDto(string Name)
{
    public IEnumerable<PermissionNodeDto> Next { get; init; } = [];
}