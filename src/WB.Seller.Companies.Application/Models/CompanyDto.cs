using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WB.Seller.Companies.Domain.Enums;
using WB.Seller.Companies.Domain.Seed;

namespace WB.Seller.Companies.Application.Models
{
    public record CompanyDto
    {
        public int Id { get; init; }
        public required string Name { get; set; }
        public bool IsActive { get; set; }
        public CompanyState State { get; set; }
        public int NumberOfTokenChecks { get; set; }
        public DateTime StateUpdated { get; set; }
    }
}
