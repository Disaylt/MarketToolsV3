namespace Identity.WebApi.Services.Interfaces
{
    public interface ICookiesContextService
    {
        public void MarkAsNew();
        public void AddSessionToken(string token);
        public void AddAccessToken(string token);
        public void DeleteAccessToken();
        public void DeleteSessionToken();
    }
}
