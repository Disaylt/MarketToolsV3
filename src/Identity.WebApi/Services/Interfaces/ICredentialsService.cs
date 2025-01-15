namespace Identity.WebApi.Services.Interfaces
{
    public interface ICredentialsService
    {
        public void RefreshCredentials(string accessToken, string refreshToken);
    }
}
