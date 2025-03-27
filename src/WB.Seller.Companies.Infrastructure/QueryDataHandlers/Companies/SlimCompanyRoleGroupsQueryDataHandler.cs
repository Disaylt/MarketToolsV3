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
using WB.Seller.Companies.Domain.Seed;

namespace WB.Seller.Companies.Infrastructure.QueryDataHandlers.Companies
{
    internal class SlimCompanyRoleGroupsQueryDataHandler(IDbConnection dbConnection)
        : IQueryDataHandler<SlimCompanyRoleGroupsQueryData, IEnumerable<GroupDto<string, CompanySlimInfoDto>>>
    {
        private readonly string _queryString = "select " +
                                               "s.\"Role\", " +
                                               "jsonb_agg(jsonb_build_object('Id', c.\"Id\", 'Name', c.\"Name\", 'Token', c.\"Token\")) AS \"Values\" " +
                                               "from companies as c " +
                                               "join subscriptions as s on s.\"CompanyId\" = c.\"Id\" " +
                                               "join users as u on u.\"SubId\" = s.\"UserId\" " +
                                               "where \"SubId\" = @SubId " +
                                               "group by s.\"Role\"";
        public async Task<IEnumerable<GroupDto<string, CompanySlimInfoDto>>> HandleAsync(SlimCompanyRoleGroupsQueryData queryData)
        {
            var dynamic =
                await dbConnection.QueryAsync(
                    _queryString,
                    new { SubId = queryData.UserId });

            var result = dynamic.Select(item => new Test
            {

                Role = item.Role,
                Values = JsonSerializer.Deserialize<TestObj[]>(item.Values.ToString())
            }).ToList();

            return [];
        }
    }

    public class Test
    {
        public int Role { get; set; }
        public TestObj[] Values { get; set; } = [];
    }

    public class TestObj
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Token { get; set; }
    }
}
