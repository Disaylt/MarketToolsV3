using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WB.Seller.Api.Companies.Domain.Seed;

namespace WB.Seller.Api.Companies.Domain.Entities
{
    public class Owner : Entity
    {
        public string UserId { get; private set; } = null!;
        public override int Id
        {
            get => throw new NullReferenceException("Id don't use for owner");
            protected set => throw new NullReferenceException("Id don't use for owner");
        }

        protected Owner()
        {

        }

        public Owner(string userId)
        {
            UserId = userId;
        }
    }
}
