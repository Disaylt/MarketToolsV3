using MarketToolsV3.ApiGateway.Constant;

namespace MarketToolsV3.ApiGateway.Services.Interfaces
{
    public interface IAuthContext
    {
        string? SessionToken { get; set; }
        string? AccessToken { get; set; }
        string? SessionId { get; set; }
        AuthState State { get; set; }
    }
}
