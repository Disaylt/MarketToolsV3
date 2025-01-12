namespace MarketToolsV3.ApiGateway.Services
{
    public class AuthContext : IAuthContext
    {
        public string? SessionToken { get; set; }
        public string? AccessToken { get; set; }
        public bool IsAuth { get; set; }
        public bool Refreshed { get; set; }
    }
}
