using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Domain.Seed
{
    public class ServiceConfiguration
    {
        public virtual int ExpireRefreshTokenHours { get; set; } = 240;
        public virtual string SecretRefreshToken { get; set; } = string.Empty;
        public virtual string DatabaseConnection { get; set; } = string.Empty;
        public virtual string? RedisConnection { get; set; }
    }
}
