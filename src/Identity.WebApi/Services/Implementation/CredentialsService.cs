using Identity.WebApi.Services.Interfaces;

namespace Identity.WebApi.Services.Implementation;

public class CredentialsService(ICookiesContextService cookiesContextService,
    ISessionContextService sessionContextService)
    : ICredentialsService
{
    public void Refresh(string accessToken, string refreshToken)
    {
        cookiesContextService.AddAccessToken(accessToken);
        cookiesContextService.AddSessionToken(refreshToken);
        cookiesContextService.MarkAsNew();
    }

    public void Remove(string sessionId)
    {
        cookiesContextService.DeleteSessionToken();
        cookiesContextService.DeleteAccessToken();
        cookiesContextService.MarkAsNew();
        sessionContextService.MarkAsDelete(sessionId);
    }
}