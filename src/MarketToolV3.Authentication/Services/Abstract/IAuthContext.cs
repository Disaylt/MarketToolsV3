using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketToolV3.Authentication.Services.Abstract
{
    public interface IAuthContext
    {
        public string? UserId { get; set; }
        public string? SessionId { get; set; }

        string GetUserIdRequired();
        string GetSessionIdRequired();
    }
}
