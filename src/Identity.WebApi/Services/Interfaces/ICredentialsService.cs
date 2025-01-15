namespace Identity.WebApi.Services.Interfaces
{
    public interface ICredentialsService
    {
        public void Refresh(string accessToken, string refreshToken);
        public void Remove(string sessionId);
    }
}
