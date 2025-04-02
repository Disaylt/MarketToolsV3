using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WB.Seller.Companies.Application.Models;
using WB.Seller.Companies.Domain.Entities;
using WB.Seller.Companies.Domain.Seed;

namespace WB.Seller.Companies.Application.Mappers
{
    internal class CompanyTransferFromEntityMapper : IFromMapper<CompanyEntity, CompanyDto>
    {
        public CompanyDto Map(CompanyEntity value)
        {
            return new()
            {
                Id = value.Id,
                Name = value.Name
            };
        }
    }
}
