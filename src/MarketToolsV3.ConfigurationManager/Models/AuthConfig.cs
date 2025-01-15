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
        public int ExpireAccessTokenMinutes { get; init; } = 15;
        public string? ValidAudience { get; init; }
        public string? ValidIssuer { get; init; }
        public bool IsCheckValidAudience { get; init; } = true;
        public bool IsCheckValidIssuer { get; init; } = true;
        public bool IsCheckExpireDate { get; init; } = true;
        public string AccessTokenName { get; init; } = "access-token";
        public string RefreshTokenName { get; init; } = "refresh-token";
        public Headers Headers { get; init; } = new();
    }

    public record Headers
    {
        public CookieActionHeader CookieAction { get; init; } = new();
        public SessionRemoveHeader SessionRemove { get; init; } = new();
    }

    public record CookieActionHeader
    {
        public string Name { get; init; } = "mp-cookie-action";
        public CookieActionHeaderValues Properties { get; init; } = new();

    }

    public record CookieActionHeaderValues
    {
        public string New { get; set; } = "new";
    }

    public record SessionRemoveHeader
    {
        public string Name { get; init; } = "mp-session-remove";
    }
}
