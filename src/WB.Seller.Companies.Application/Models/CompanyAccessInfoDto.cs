using WB.Seller.Companies.Domain.Enums;

namespace WB.Seller.Companies.Application.Models;

public record CompanyAccessInfoDto
{
    public required SubscriptionRole Role { get; init; }
    public IEnumerable<PermissionSettingNodeDto> Permissions { get; init; } = [];
}