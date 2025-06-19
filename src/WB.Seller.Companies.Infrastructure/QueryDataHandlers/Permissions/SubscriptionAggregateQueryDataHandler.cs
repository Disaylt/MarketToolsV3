using Dapper;
using System.Data;
using WB.Seller.Companies.Application.Models;
using WB.Seller.Companies.Application.QueryData.Permissions;
using WB.Seller.Companies.Domain.Enums;
using WB.Seller.Companies.Domain.Seed;

namespace WB.Seller.Companies.Infrastructure.QueryDataHandlers.Permissions;

public class SubscriptionAggregateQueryDataHandler(IDbConnection dbConnection)
: IQueryDataHandler<SubscriptionAggregateQueryData, SubscriptionAggregateDto>
{
    private const string QueryString = $@"select
                                        s.role as {nameof(SubscriptionAggregateDto.Role)},
                                        p.status as {nameof(PermissionDto.Status)},
                                        p.path as {nameof(PermissionDto.Path)},
                                        p.id as {nameof(PermissionDto.Id)}
                                        from subscriptions as s
                                        join permissions as p on p.subscription_id = s.id
                                        where s.user_id = @{nameof(SubscriptionAggregateQueryData.UserId)}";

    public async Task<SubscriptionAggregateDto> HandleAsync(SubscriptionAggregateQueryData queryData)
    {
        var t =  await dbConnection.QueryAsync<(SubscriptionRole Role, PermissionStatus Status, string Path, int Id) >(QueryString, queryData);

        return new SubscriptionAggregateDto
        {
            Role = SubscriptionRole.Owner
        };
    }
}