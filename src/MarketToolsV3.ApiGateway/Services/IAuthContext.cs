namespace MarketToolsV3.ApiGateway.Services
{
    public interface IAuthContext
    {
        string? SessionToken { get; set; }
        string? AccessToken { get; set; }
    }
}
