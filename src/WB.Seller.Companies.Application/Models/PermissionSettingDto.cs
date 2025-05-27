using WB.Seller.Companies.Domain.Enums;

namespace WB.Seller.Companies.Application.Models;

public record PermissionSettingDto
{
    public required string View { get; init; }
    public required string Name { get; init; }
    public PermissionStatus Status { get; init; } = PermissionStatus.None;
    public List<PermissionSettingDto> PermissionSettings { get; init; } = [];
}