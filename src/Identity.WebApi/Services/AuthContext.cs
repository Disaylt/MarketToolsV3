using System.Net;
using Identity.Domain.Seed;

namespace Identity.WebApi.Services
{
    public class AuthContext : IAuthContext
    {
        public string? UserId { get; set; }
        public string? SessionId { get; set; }

        public string GetSessionIdRequired()
        {
            return SessionId ?? throw new RootServiceException(HttpStatusCode.BadRequest, "ID сессии не найден.");
        }

        public string GetUserIdRequired()
        {
            return UserId ?? throw new RootServiceException(HttpStatusCode.BadRequest, "ID пользователя не найден.");
        }
    }
}
