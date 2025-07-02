using WB.Seller.Companies.Domain.Enums;

namespace WB.Seller.Companies.Application.Models;

public record SubscriptionAggregateDto
{
    public required SubscriptionRole Role { get; init; }
    public IEnumerable<PermissionDto> Permissions { get; init; } = [];
}