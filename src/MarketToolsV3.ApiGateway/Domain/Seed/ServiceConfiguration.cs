namespace MarketToolsV3.ApiGateway.Domain.Seed
{
    public class ServiceConfiguration
    {
        public RedisConfig SharedIdentityRedisConfig { get; set; } = new();
    }

    public class RedisConfig
    {
        public string? Host { get; set; }
        public int Port { get; set; }
        public string? Password { get; set; }
        public string? User { get; set; }
        public int Database { get; set; }

    }
}
