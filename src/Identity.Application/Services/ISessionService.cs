using Identity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Services
{
    public interface ISessionService
    {
        Task<Session> AddAsync(Session session, CancellationToken cancellationToken);

        Task UpdateAsync(Session session, string token, string userAgent = "Unknown",
            CancellationToken cancellationToken = default);
        Task<IEnumerable<Session>> GetActiveSessionsAsync(string userId, CancellationToken cancellationToken);
        Task DeleteAsync(string id, CancellationToken cancellationToken);
    }
}
