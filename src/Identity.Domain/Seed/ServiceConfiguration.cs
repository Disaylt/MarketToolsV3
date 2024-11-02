using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Domain.Seed
{
    public class ServiceConfiguration
    {
        public int ExpireAccessTokenMinutes { get; set; } = 15;
        public int ExpireRefreshTokenHours { get; set; } = 240;
        public string ValidAudience { get; set; } = string.Empty;
        public string ValidIssuer { get; set; } = string.Empty;
        public string SecretRefreshToken { get; set; } = string.Empty;
        public string SecretAccessToken { get; set; } = string.Empty;
        public string Database { get; set; } = string.Empty;
        public string Redis { get; set; } = string.Empty;
        public string ElasticUrl { get; set; } = string.Empty;
        public string ElasticUser { get; set; } = string.Empty;
        public string ElasticPassword { get; set; } = string.Empty;
    }
}
