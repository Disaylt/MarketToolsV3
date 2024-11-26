using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Domain.Seed
{
    public class ServiceConfiguration
    {
        public virtual int ExpireAccessTokenMinutes { get; set; } = 15;
        public virtual int ExpireRefreshTokenHours { get; set; } = 240;
        public virtual string ValidAudience { get; set; } = string.Empty;
        public virtual string ValidIssuer { get; set; } = string.Empty;
        public virtual string SecretRefreshToken { get; set; } = string.Empty;
        public virtual string SecretAccessToken { get; set; } = string.Empty;
        public virtual string Database { get; set; } = string.Empty;
        public virtual string Redis { get; set; } = string.Empty;
    }
}
