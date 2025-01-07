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
        public DateTime CreateDate { get; private set; } = DateTime.UtcNow;

        private readonly List<Session> _sessions = [];
        public IReadOnlyCollection<Session> Sessions => _sessions.AsReadOnly();
    }
}
