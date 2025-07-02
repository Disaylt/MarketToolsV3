using MarketToolsV3.PermissionStore.Application.Enums;

namespace MarketToolsV3.PermissionStore.Application.Models;

public class PermissionSettingNodeViewModel
{
    public string? Path { get; set; }
    public PermissionStatusEnum Status { get; set; }
    public bool RequireUse { get; set; }
    public required string? Name { get; set; }
    public string? View { get; set; }
    public IEnumerable<PermissionSettingNodeViewModel> Nodes { get; set; } = [];
}