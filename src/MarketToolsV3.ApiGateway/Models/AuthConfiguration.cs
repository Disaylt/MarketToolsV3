namespace MarketToolsV3.ApiGateway.Models
{
    public class AuthConfiguration
    {
        public string AccessTokenName { get; set; } = "access-token";
        public string RefreshTokenName { get; set; } = "refresh-token";
    }
}
