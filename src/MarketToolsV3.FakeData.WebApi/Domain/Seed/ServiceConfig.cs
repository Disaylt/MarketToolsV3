namespace MarketToolsV3.FakeData.WebApi.Domain.Seed
{
    public class ServiceConfig
    {
        public bool CanCreateFakeData { get; set; }
        public string DatabaseConnection { get; set; } = string.Empty;
        public string? BaseAddress { get; set; }
    }
}
