using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WB.Seller.Companies.OpenApi.Domain.Seed;

namespace WB.Seller.Companies.OpenApi.Domain.Entities
{
    public class CompanyEntity : Entity
    {
        public string? Token { get; set; }
    }
}
