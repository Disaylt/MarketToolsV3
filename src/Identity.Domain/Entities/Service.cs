using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Domain.Entities
{
    public class Service
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int ProviderId { get; set; }
        public required string IdentityId { get; set; }
        public IdentityPerson Identity { get; set; } = null!;

        public List<ServiceClaim> Claims { get; set; } = [];
        public List<ServiceRole> Roles { get; set; } = [];
    }
}
