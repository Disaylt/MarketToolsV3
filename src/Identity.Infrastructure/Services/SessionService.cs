using Identity.Domain.Seed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Identity.Application.Services;
using Identity.Domain.Entities;
using Identity.Domain.Events;
using Identity.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Identity.Infrastructure.Services
{
    public class SessionService(IRepository<Session> sessionsRepository,
        IOptions<ServiceConfiguration> options,
        IEventRepository eventsRepository)
        : ISessionService
    {
        private readonly ServiceConfiguration _configuration = options.Value;

        public async Task<Session> AddAsync(Session session, CancellationToken cancellationToken = default)
        {
            await sessionsRepository.AddAsync(session, cancellationToken);
            await sessionsRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            SessionCreated newSessionEvent = new SessionCreated(session);
            eventsRepository.AddNotification(newSessionEvent);

            return session;
        }

        public async Task UpdateAsync(Session session, string token, string userAgent = "Unknown", CancellationToken cancellationToken = default)
        {
            session.Token = token;
            session.UserAgent = userAgent;
            session.Updated = DateTime.UtcNow;

            await sessionsRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }

        public async Task<IEnumerable<Session>> GetActiveSessionsAsync(string identityId, CancellationToken cancellationToken = default)
        {
            return await sessionsRepository
                .AsQueryable()
                .Where(e => DateTime.UtcNow - e.Updated < TimeSpan.FromHours(_configuration.ExpireRefreshTokenHours)
                            && e.IsActive
                            && e.IdentityId == identityId)
                .ToListAsync(cancellationToken);
        }

        public async Task DeleteAsync(string id, CancellationToken cancellationToken = default)
        {
            Session session = await sessionsRepository.FindByIdRequiredAsync(id, cancellationToken);
            await sessionsRepository.DeleteAsync(session, cancellationToken);
            await sessionsRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
