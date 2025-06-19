using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Azure.Core.Serialization;
using Dapper;
using Npgsql;
using WB.Seller.Companies.Application.Models;
using WB.Seller.Companies.Application.QueryData.Companies;
using WB.Seller.Companies.Domain.Enums;
using WB.Seller.Companies.Domain.Seed;

namespace WB.Seller.Companies.Infrastructure.QueryDataHandlers.Companies
{
    internal class SlimCompanyRoleGroupsQueryDataHandler(IDbConnection dbConnection)
        : IQueryDataHandler<SlimCompanyRoleGroupsQueryData, IEnumerable<GroupDto<SubscriptionRole, CompanySlimInfoDto>>>
    {
        private const string QueryString = @$"select
                                            s.role as Key,
                                            jsonb_agg(
                                                jsonb_build_object(
                                                    '{nameof(CompanySlimInfoDto.Id)}', c.id, 
                                                    '{nameof(CompanySlimInfoDto.Name)}', c.name)) 
                                                as Values
                                            from companies as c
                                            join subscriptions as s on s.company_id = c.id
                                            join users as u on u.sub_id = s.user_id
                                            where sub_id = @{nameof(SlimCompanyRoleGroupsQueryData.UserId)}
                                            group by s.role";

        public async Task<IEnumerable<GroupDto<SubscriptionRole, CompanySlimInfoDto>>> HandleAsync(SlimCompanyRoleGroupsQueryData queryData)
        {
            return await dbConnection.QueryAsync<GroupDto<SubscriptionRole, CompanySlimInfoDto>>(QueryString, queryData);
        }
    }
}
