using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using WB.Seller.Companies.Application.Models;

namespace WB.Seller.Companies.Application.Queries
{
    public class GetSlimCompaniesQuery : IRequest<IEnumerable<GroupDto<string, CompanySlimInfoDto>>>
    {
        public required string UserId { get; set; }
    }
}
