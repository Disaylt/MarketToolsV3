using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketToolsV3.ConfigurationManager.Models
{
    public record AuthConfig
    {
        public string AuthSecret { get; init; } = string.Empty;
        public string ExpireAccessTokenMinutes { get; init; } = string.Empty;
        public string? ValidAudience { get; init; }
        public string? ValidIssuer { get; init; }
        public bool IsCheckValidAudience { get; init; } = true;
        public bool IsCheckValidIssuer { get; init; } = true;
        public bool IsCheckExpireDate { get; init; } = true;
    }
}
