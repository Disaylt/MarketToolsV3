using Identity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Services.Abstract
{
    public interface ISessionService
    {
        Task<Session> AddAsync(Session session, CancellationToken cancellationToken);

        Task UpdateAsync(Session session, string token, CancellationToken cancellationToken,
            string userAgent = "Unknown");
        Task<IEnumerable<Session>> GetActiveSessionsAsync(string identityId, CancellationToken cancellationToken);
        Task DeleteAsync(string id, CancellationToken cancellationToken);
        Task DeactivateAsync(string id, CancellationToken cancellationToken);
    }
}
