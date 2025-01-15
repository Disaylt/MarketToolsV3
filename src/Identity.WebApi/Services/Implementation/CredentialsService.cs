using Identity.WebApi.Services.Interfaces;

namespace Identity.WebApi.Services.Implementation;

public class CredentialsService(ICookiesContextService cookiesContextService)
    : ICredentialsService
{
    public void RefreshCredentials(string accessToken, string refreshToken)
    {
        cookiesContextService.AddAccessToken(accessToken);
        cookiesContextService.AddSessionToken(refreshToken);
        cookiesContextService.MarkAsNew();
    }
}