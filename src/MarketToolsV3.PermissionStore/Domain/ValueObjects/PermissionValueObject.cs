namespace MarketToolsV3.PermissionStore.Domain.ValueObjects;

public class PermissionValueObject
{
    public required string Path { get; init; }
    public bool RequireUse { get; init; }
    public IEnumerable<string> AvailableModules { get; init; } = [];
}