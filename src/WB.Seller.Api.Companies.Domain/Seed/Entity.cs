using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WB.Seller.Api.Companies.Domain.Seed
{
    public abstract class Entity
    {
        public virtual int Id { get; protected set; }
    }
}
