using WB.Seller.Companies.Domain.Enums;

namespace WB.Seller.Companies.Application.Models;

public record PermissionSettingNodeDto
{
    public required string Path { get; init; }
    public required string Name { get; init; }
    public bool RequireUse { get; init; }
    public PermissionStatus Status { get; init; } = PermissionStatus.None;
    public IReadOnlyCollection<PermissionSettingNodeDto> ChildNodes { get; init; } = [];
}