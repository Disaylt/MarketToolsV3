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
        public virtual RedisConfig IdentityRedisConfig { get; set; } = new();
        public virtual RedisConfig SharedIdentityRedisConfig { get; set; } = new();
    }

    public class RedisConfig
    {
        public string? Host { get; set; }
        public int Port { get; set; }
        public string? Password { get; set; }
        public string? User {get; set; }
        public int Database { get; set; }

    }
}
