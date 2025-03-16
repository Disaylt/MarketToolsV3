using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Domain.Entities
{
    public class ServiceRole
    {
        public int Id { get; set; }
        public int Value { get; set; }

        public int ServiceId { get; set; }
        public Service Service { get; set; } = null!;
    }
}
