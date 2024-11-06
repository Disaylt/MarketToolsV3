namespace Identity.WebApi.Models
{
    public class WebApiConfiguration
    {
        public virtual string AccessTokenName { get; set; } = "access-token";
        public virtual string RefreshTokenName { get; set; } = "refresh-token";
    }
}
