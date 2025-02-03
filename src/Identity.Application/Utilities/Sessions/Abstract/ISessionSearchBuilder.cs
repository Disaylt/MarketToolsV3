using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Utilities.Sessions.Abstract
{
    internal interface ISessionSearchBuilder<TSession> where TSession : ISessionSearchModel
    {
        ISessionSearchBuilder<TSession> UseCache();
        ISessionSearchBuilder<TSession> UseDatabase();
        ISessionSearchBuilder<TSession> SetCacheTime(TimeSpan timeSpan);
        Task<TSession> SearchAsync();
    }
}
