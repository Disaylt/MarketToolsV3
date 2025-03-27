using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using WB.Seller.Companies.Application.Models;
using WB.Seller.Companies.Domain.Enums;

namespace WB.Seller.Companies.Application.Queries
{
    public class GetSlimCompaniesQuery : IRequest<IEnumerable<GroupDto<EnumViewDto<SubscriptionRole>, CompanySlimInfoDto>>>
    {
        public required string UserId { get; set; }
    }
}
