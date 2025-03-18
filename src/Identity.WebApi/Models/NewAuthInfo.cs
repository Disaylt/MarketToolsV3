namespace Identity.WebApi.Models
{
    public class NewAuthInfo
    {
        public required string RefreshToken { get; set; }
        public int? ProviderType { get; set; }
        public int? ProviderId { get; set; }
    }
}
