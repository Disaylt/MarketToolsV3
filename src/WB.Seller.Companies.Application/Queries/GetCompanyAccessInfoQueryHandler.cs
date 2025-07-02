using MediatR;
using WB.Seller.Companies.Application.Models;
using WB.Seller.Companies.Application.QueryData.Permissions;
using WB.Seller.Companies.Application.Services.Abstract;
using WB.Seller.Companies.Domain.Seed;

namespace WB.Seller.Companies.Application.Queries;

public class GetCompanyAccessInfoQueryHandler(
    IQueryDataHandler<SubscriptionAggregateQueryData, SubscriptionAggregateDto> queryDataHandler)
: IRequestHandler<GetCompanyAccessInfoQuery, AccessInfoDto>
{
    public async Task<AccessInfoDto> Handle(GetCompanyAccessInfoQuery request, CancellationToken cancellationToken)
    {
        SubscriptionAggregateDto subscriptionAggregate = await queryDataHandler.HandleAsync(new()
        {
            CompanyId = request.CompanyId,
            UserId = request.UserId
        });

        return new()
        {
            Role = subscriptionAggregate.Role,
            Permissions = subscriptionAggregate
                .Permissions
                .ToDictionary(x=> x.Path, x => x.Status)
        };
    }
}