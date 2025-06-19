using WB.Seller.Companies.Application.Models;
using WB.Seller.Companies.Domain.Seed;

namespace WB.Seller.Companies.Application.QueryData.Permissions;

public class SubscriptionAggregateQueryData : IQueryData<SubscriptionAggregateDto>
{
    public required string UserId { get; set; }
}