using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Domain.Entities
{
    public class Session
    {
        public string Id { get; private set; }
        public string? Token { get; set; }
        public bool IsActive { get; set; }
        public DateTime Created { get; private set; } = DateTime.UtcNow;
        public DateTime Updated { get; set; } = DateTime.UtcNow;
        public IdentityPerson Identity { get; private set; } = null!;
        public string IdentityId { get; private set; } = null!;
        public string UserAgent { get; set; } = null!;

        protected Session()
        {
            IsActive = true;
            Id = Guid.NewGuid().ToString();
        }

        public Session(string identityId, string userAgent) : this()
        {
            IdentityId = identityId;
            UserAgent = userAgent;
        }
    }
}
