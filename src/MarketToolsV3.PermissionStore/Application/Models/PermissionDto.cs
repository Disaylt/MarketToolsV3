namespace MarketToolsV3.PermissionStore.Application.Models;

public class PermissionDto
{
    public required string Path { get; set; }
    public bool RequireUse { get; set; }
    public IEnumerable<string> AvailableModules { get; set; } = [];
}