namespace MarketToolsV3.FakeData.WebApi.Application.Models
{
    public record JsonRandomModel
    {
        public required string Type { get; init; }
        public int Min { get; init; }
        public int Max { get; init; }
    }
}
