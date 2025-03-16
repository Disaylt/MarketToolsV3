using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Domain.Entities
{
    public class ServiceClaim
    {
        public int Id { get; set; }

        public int Type { get; set; }
        public int Value { get; set; }

        public int CategoryId { get; set; }
        public int ProviderId { get; set; }
        public Service Service { get; set; } = null!;
    }
}
