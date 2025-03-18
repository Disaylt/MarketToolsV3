using Identity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Identity.Domain.Seed;

namespace Identity.Application.QueryObjects
{
    public class FindServiceQueryObject : IQueryObject<Service>
    {
        public int ProviderType { get; set; }
        public int ProviderId { get; set; }
    }
}
