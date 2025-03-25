using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WB.Seller.Companies.Application.Models;
using WB.Seller.Companies.Application.Seed;

namespace WB.Seller.Companies.Application.Commands
{
    public class CreateCompanyCommand : ICommand<CompanyDto>
    {
        public required string UserId { get; set; }
        public required string Name { get; set; }
        public string? Token { get; set; }
        public string? Description { get; set; }
    }
}
