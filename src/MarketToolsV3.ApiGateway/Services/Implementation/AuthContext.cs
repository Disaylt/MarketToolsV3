using MarketToolsV3.ApiGateway.Constant;
using MarketToolsV3.ApiGateway.Services.Interfaces;

namespace MarketToolsV3.ApiGateway.Services.Implementation
{
    public class AuthContext : IAuthContext
    {
        public string? SessionToken { get; set; }
        public string? AccessToken { get; set; }
        public bool IsAuth { get; set; }
        public bool Refreshed { get; set; }
        public AuthState State { get; set; } = AuthState.None;
    }
}
