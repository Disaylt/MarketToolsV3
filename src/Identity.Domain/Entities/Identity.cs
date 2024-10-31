using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Identity.Domain.Entities
{
    public class Identity : IdentityUser
    {
        public DateTime CreateDate { get; } = DateTime.UtcNow;
    }
}
