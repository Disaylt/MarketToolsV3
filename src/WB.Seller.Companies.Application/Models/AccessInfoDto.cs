using WB.Seller.Companies.Domain.Enums;

namespace WB.Seller.Companies.Application.Models;

public record AccessInfoDto
{
    public required SubscriptionRole Role { get; init; }
    public Dictionary<string, PermissionStatus> Permissions { get; init; } = [];
}