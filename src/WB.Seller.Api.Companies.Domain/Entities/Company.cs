using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WB.Seller.Api.Companies.Domain.Entities
{
    public class Company
    {
        public string Name { get; private set; }
        public string? Token { get; private set; }

        protected Company()
        {
            Name = null!;
        }

        public Company(string name, string? token) : this()
        {
            Name = name;
            Token = token;
        }
    }
}
