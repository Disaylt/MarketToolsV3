namespace MarketToolsV3.FakeData.WebApi.Infrastructure.Models
{
    public record HttpClientHandlerInfoModel
    {
        public DateTime Created { get; } = DateTime.UtcNow;
        public required SocketsHttpHandler Handler { get; init; }
    }
}
