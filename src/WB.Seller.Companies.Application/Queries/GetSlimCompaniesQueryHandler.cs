using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using WB.Seller.Companies.Application.Models;
using WB.Seller.Companies.Application.QueryData.Companies;
using WB.Seller.Companies.Domain.Enums;
using WB.Seller.Companies.Domain.Seed;

namespace WB.Seller.Companies.Application.Queries
{
    public class GetSlimCompaniesQueryHandler(
        IQueryDataHandler<SlimCompanyRoleGroupsQueryData, IEnumerable<GroupDto<SubscriptionRole, CompanySlimInfoDto>>> queryDataHandler)
        : IRequestHandler<GetSlimCompaniesQuery, IEnumerable<GroupDto<EnumViewDto<SubscriptionRole>, CompanySlimInfoDto>>>
    {
        public async Task<IEnumerable<GroupDto<EnumViewDto<SubscriptionRole>, CompanySlimInfoDto>>> Handle(GetSlimCompaniesQuery request, CancellationToken cancellationToken)
        {
            var queryData = new SlimCompanyRoleGroupsQueryData
            {
                UserId = request.UserId
            };

            var result = await queryDataHandler.HandleAsync(queryData);

            return result
                .Select(x => new GroupDto<EnumViewDto<SubscriptionRole>, CompanySlimInfoDto>
                {
                    Key = new EnumViewDto<SubscriptionRole>(x.Key),
                    Values = x.Values
                });
        }
    }
}
