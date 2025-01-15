namespace Identity.WebApi.Services.Interfaces
{
    public interface IAuthContext
    {
        public string? UserId { get; set; }
        public string? SessionId { get; set; }

        string GetUserIdRequired();
        string GetSessionIdRequired();
    }
}
