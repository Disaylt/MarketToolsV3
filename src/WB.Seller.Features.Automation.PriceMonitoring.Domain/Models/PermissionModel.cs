namespace WB.Seller.Features.Automation.PriceMonitoring.Domain.Models;

public record PermissionModel
{
    public required string Path { get; init; }
    public IReadOnlyCollection<PermissionModel> ChildPermissions { get; init; } = [];
}