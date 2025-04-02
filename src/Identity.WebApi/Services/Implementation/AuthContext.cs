using System.Net;
using Identity.Domain.Seed;
using Identity.WebApi.Services.Interfaces;

namespace Identity.WebApi.Services.Implementation
{
    public class AuthContext : IAuthContext
    {
        public string? UserId { get; set; }
        public string? SessionId { get; set; }

        public string GetSessionIdRequired()
        {
            return SessionId ?? throw new RootServiceException()
                .AddMessages("ID сессии не найден.");
        }

        public string GetUserIdRequired()
        {
            return UserId ?? throw new RootServiceException()
                .AddMessages("ID пользователя не найден.");
        }
    }
}
