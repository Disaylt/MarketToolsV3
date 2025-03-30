using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WB.Seller.Companies.OpenApi.Domain.Seed
{
    public class Entity
    {
        public virtual int Id { get; protected set; }
        public DateTimeOffset Created { get; private set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset Updated { get; protected set; }
    }
}
