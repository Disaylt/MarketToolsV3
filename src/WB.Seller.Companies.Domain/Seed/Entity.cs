using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WB.Seller.Companies.Domain.Seed
{
    public abstract class Entity
    {
        public virtual int Id { get; protected set; }
        public DateTime Created { get; private set; } = DateTime.UtcNow;
        public DateTime Updated { get; protected set; }
    }
}
