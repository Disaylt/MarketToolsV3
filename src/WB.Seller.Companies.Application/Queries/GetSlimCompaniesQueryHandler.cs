using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using WB.Seller.Companies.Application.Models;
using WB.Seller.Companies.Application.QueryData.Companies;
using WB.Seller.Companies.Domain.Seed;

namespace WB.Seller.Companies.Application.Queries
{
    public class GetSlimCompaniesQueryHandler(
        IQueryDataHandler<SlimCompanyRoleGroupsQueryData, IEnumerable<GroupDto<string, CompanySlimInfoDto>>> queryDataHandler)
        : IRequestHandler<GetSlimCompaniesQuery, IEnumerable<GroupDto<string, CompanySlimInfoDto>>>
    {
        public async Task<IEnumerable<GroupDto<string, CompanySlimInfoDto>>> Handle(GetSlimCompaniesQuery request, CancellationToken cancellationToken)
        {
            var queryData = new SlimCompanyRoleGroupsQueryData
            {
                UserId = request.UserId
            };

            return await queryDataHandler.HandleAsync(queryData);
        }
    }
}
