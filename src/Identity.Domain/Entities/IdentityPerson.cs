using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Identity.Domain.Entities
{
    public class IdentityPerson : IdentityUser
    {
        public string? ConfirmationCode { get; set; }
        public DateTime ConfirmationCodeUpdated { get; set; }
        public DateTime CreateDate { get; private set; } = DateTime.UtcNow;
        public List<Session> Sessions { get; set; } = [];
        public List<Module> Modules { get; set; } = [];
    }
}
