using MarketToolsV3.ApiGateway.Constant;

namespace MarketToolsV3.ApiGateway.Services.Interfaces
{
    public interface IAuthContext
    {
        bool IsAuth { get; set; }
        bool Refreshed { get; set; }
        string? SessionToken { get; set; }
        string? AccessToken { get; set; }
        string? SessionId { get; set; }
        AuthState State { get; set; }
    }
}
