namespace MarketToolsV3.PermissionStore.Application.Models;

public class ModuleGroupDto
{
    public required string Module { get; set; }
    public IReadOnlyCollection<ModulePermissionDto> Permissions { get; set; } = [];
}