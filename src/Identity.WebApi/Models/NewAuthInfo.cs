namespace Identity.WebApi.Models
{
    public class NewAuthInfo
    {
        public required string RefreshToken { get; set; }
        public string? ModulePath { get; set; }
        public int? ModuleId { get; set; }
    }
}
