using System.Security.Claims;

namespace MarketToolsV3.ApiGateway.Extensions
{
    internal static class ClaimsExtensions
    {
        public static string? FindByType(this IEnumerable<Claim> claims, string type)
        {
            return claims.FirstOrDefault(x => x.Type == type)?.Value;
        }
    }
}
