using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WB.Seller.Companies.Application.Models;
using WB.Seller.Companies.Domain.Enums;
using WB.Seller.Companies.Domain.Seed;

namespace WB.Seller.Companies.Application.QueryData.Companies
{
    public class SlimCompanyRoleGroupsQueryData : IQueryData<IEnumerable<GroupDto<SubscriptionRole, CompanySlimInfoDto>>>
    {
        public required string UserId { get; set; }
    }
}
