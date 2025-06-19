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
    private const string QueryString = $@"SELECT 
            s.role as {nameof(SubscriptionAggregateDto.Role)},
            (
                SELECT json_agg(json_build_object(
                    '{nameof(PermissionDto.Id)}', p.id,
                    '{nameof(PermissionDto.Status)}', p.status,
                    '{nameof(PermissionDto.Path)}', p.path
                ))
                FROM Permissions as p
                where p.subscription_id = s.id
            ) AS {nameof(SubscriptionAggregateDto.Permissions)}
            from subscriptions as s
            where s.user_id = @{nameof(SubscriptionAggregateQueryData.UserId)}
            and s.company_id = @{nameof(SubscriptionAggregateQueryData.CompanyId)}";

    public async Task<SubscriptionAggregateDto> HandleAsync(SubscriptionAggregateQueryData queryData)
    {
        return await dbConnection.QuerySingleAsync<SubscriptionAggregateDto>(QueryString, queryData);
    }
}