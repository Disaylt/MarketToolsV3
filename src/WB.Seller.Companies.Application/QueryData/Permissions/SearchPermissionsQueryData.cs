using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WB.Seller.Companies.Application.Models;
using WB.Seller.Companies.Domain.Seed;

namespace WB.Seller.Companies.Application.QueryData.Permissions
{
    public class SearchPermissionsQueryData : IQueryData<IEnumerable<PermissionDto>>
    {
        public int CompanyId { get; set; }
        public required string SubscriberId { get; set; }
    }
}
