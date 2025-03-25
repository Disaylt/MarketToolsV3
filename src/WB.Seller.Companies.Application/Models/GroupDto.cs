using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WB.Seller.Companies.Application.Models
{
    public class GroupDto<TKey, TValue>
    {
        public required TKey Key { get; set; }
        public required IEnumerable<TValue> Values { get; set; }
    }
}
