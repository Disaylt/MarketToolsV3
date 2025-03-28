namespace MarketToolsV3.ApiGateway.Models.Tokens
{
    public class AccessToken : BaseToken
    {
        public string? SessionId { get; set; }
        public string? Id { get; set; }
        public ModuleAuthInfoDto? ModuleAuthInfo { get; set; }
    }

    public class ModuleAuthInfoDto
    {
        public required string Path { get; set; }
        public int Id { get; set; }
    }
}
