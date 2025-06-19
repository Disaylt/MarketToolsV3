using System.Data;
using Dapper;
using WB.Seller.Companies.Application.Models;
using WB.Seller.Companies.Application.QueryData.Permissions;
using WB.Seller.Companies.Domain.Seed;

namespace WB.Seller.Companies.Infrastructure.QueryDataHandlers.Permissions;

public class SearchPermissionsQueryDataHandler(IDbConnection dbConnection)
    : IQueryDataHandler<SearchPermissionsQueryData, IEnumerable<PermissionDto>>
{
    private const string QueryString = $@"select
                                        p.id as {nameof(PermissionDto.Id)},
                                        p.status as {nameof(PermissionDto.Status)},
                                        p.type as {nameof(PermissionDto.Path)}
                                        from permissions as p
                                        join subscription as s on p.subscription_id = s.id
                                        where s.user_id = @{nameof(SearchPermissionsQueryData.SubscriberId)}
                                        and s.company_id = @{nameof(SearchPermissionsQueryData.CompanyId)}";

    public async Task<IEnumerable<PermissionDto>> HandleAsync(SearchPermissionsQueryData queryData)
    {
        return await dbConnection.QueryAsync<PermissionDto>(QueryString, queryData);
    }
}