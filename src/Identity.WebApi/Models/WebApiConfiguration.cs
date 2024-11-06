namespace Identity.WebApi.Models
{
    public class WebApiConfiguration
    {
        public string AccessTokenName { get; set; } = "access-token";
        public string RefreshTokenName { get; set; } = "refresh-token";
    }
}
