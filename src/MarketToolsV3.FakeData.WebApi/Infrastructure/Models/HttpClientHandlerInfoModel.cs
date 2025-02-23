namespace MarketToolsV3.FakeData.WebApi.Infrastructure.Models
{
    public record HttpClientHandlerInfoModel
    {
        public required string Id { get; init; }
        public DateTime Created { get; } = DateTime.UtcNow;
        public required SocketsHttpHandler Handler { get; init; }
    }
}
