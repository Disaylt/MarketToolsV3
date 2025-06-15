namespace WB.Seller.Features.Automation.PriceManager.Domain.Models;

public record PermissionModel
{
    public required string Path { get; init; }
    public IReadOnlyCollection<PermissionModel> ChildPermissions { get; init; } = [];
}