using Identity.WebApi.Models;

namespace Identity.WebApi.Services
{
    public interface ISessionStateService
    {
        Task<SessionValidStatusDto> GetSessionValidStatus(string? refreshToken, CancellationToken cancellationToken = default);
    }
}
