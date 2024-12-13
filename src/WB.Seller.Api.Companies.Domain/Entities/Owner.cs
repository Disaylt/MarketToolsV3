using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WB.Seller.Api.Companies.Domain.Entities
{
    public class Owner
    {
        public string Id { get; private set; } = null!;

        protected Owner()
        {

        }

        public Owner(string id)
        {
            Id = id;
        }
    }
}
