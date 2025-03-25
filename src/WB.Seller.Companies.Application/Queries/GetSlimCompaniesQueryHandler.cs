using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using WB.Seller.Companies.Application.Models;

namespace WB.Seller.Companies.Application.Queries
{
    public class GetSlimCompaniesQueryHandler 
        : IRequestHandler<GetSlimCompaniesQuery, IEnumerable<GroupDto<string, CompanySlimInfoDto>>>
    {
        public async Task<IEnumerable<GroupDto<string, CompanySlimInfoDto>>> Handle(GetSlimCompaniesQuery request, CancellationToken cancellationToken)
        {
            List<string> test = ["1", "2", "1", "3", "2", "1"];

            return test
                .GroupBy(x => x)
                .Select(g => new GroupDto<string, CompanySlimInfoDto>
                {
                    Key = g.Key,
                    Values = g.Select(v=> new CompanySlimInfoDto { Id = 1, Name = $"test - {v}" })
                });
        }
    }
}
