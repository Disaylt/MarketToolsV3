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
        public DateTime CreateDate { get; } = DateTime.UtcNow;

        private List<Session> _sessions = new();
        public IReadOnlyCollection<Session> Sessions => _sessions.AsReadOnly();
    }
}
