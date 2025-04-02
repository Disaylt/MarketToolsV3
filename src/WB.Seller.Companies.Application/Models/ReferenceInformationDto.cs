using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WB.Seller.Companies.Domain.Enums;

namespace WB.Seller.Companies.Application.Models
{
    public class ReferenceInformationDto
    {
        public EnumsInformation Enum { get; set; } = new();
    }

    public class EnumsInformation
    {
        public IEnumerable<EnumViewDto<SubscriptionRole>> SubscriptionRoles { get; set; } = [];
    }
}
