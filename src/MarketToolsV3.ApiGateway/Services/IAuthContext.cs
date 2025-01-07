namespace MarketToolsV3.ApiGateway.Services
{
    public interface IAuthContext
    {
        bool IsAuth { get; set; }
        string? SessionToken { get; set; }
        string? AccessToken { get; set; }
    }
}
