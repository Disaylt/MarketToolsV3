namespace WB.Seller.Features.Automation.PriceManager.Application.Models;

public record PermissionInfoDto
{
    public required string Path { get; init; }
    public bool RequireUse { get; init; }
    public IEnumerable<string> AvailableModules { get; init; } = [];
}