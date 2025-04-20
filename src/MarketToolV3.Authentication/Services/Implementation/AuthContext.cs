using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketToolV3.Authentication.Services.Abstract;

namespace MarketToolV3.Authentication.Services.Implementation
{
    internal class AuthContext : IAuthContext
    {
        public string? UserId { get; set; }
        public string? SessionId { get; set; }

        public string GetSessionIdRequired()
        {
            return SessionId ?? throw new NullReferenceException("ID сессии не найден.");
        }

        public string GetUserIdRequired()
        {
            return UserId ?? throw new NullReferenceException("ID пользователя не найден.");
        }
    }
}
