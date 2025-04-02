using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WB.Seller.Companies.Domain.Enums;
using WB.Seller.Companies.Domain.Extensions;

namespace WB.Seller.Companies.Application.Models
{
    public class EnumViewDto<T>(T value)
        where T : Enum
    {
        public T Value { get; } = value;
        public string View { get; } = value.GetDescription();
    }
}
