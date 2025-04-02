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
    public class GetReferenceInformationQueryHandler : IRequestHandler<GetReferenceInformationQuery, ReferenceInformationDto>
    {
        public Task<ReferenceInformationDto> Handle(GetReferenceInformationQuery request, CancellationToken cancellationToken)
        {
            var result = new ReferenceInformationDto
            {
                Enum = new()
                {
                    SubscriptionRoles = Enum.GetValues<SubscriptionRole>()
                        .Select(x=> new EnumViewDto<SubscriptionRole>(x))
                }
            };

            return Task.FromResult(result);
        }
    }
}
