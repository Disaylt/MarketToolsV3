using Identity.Domain.Seed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Identity.Domain.Entities;
using Identity.Domain.Events;
using Identity.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Identity.Application.Services.Abstract;
using Identity.Application.Models;

namespace Identity.Infrastructure.Services.Implementation
{
    public class SessionService(IRepository<Session> sessionsRepository,
        IOptions<ServiceConfiguration> options,
        IEventRepository eventsRepository,
        IAccessTokenBlacklistService accessTokenBlacklistService,
        ITokenService<JwtRefreshTokenDto> refreshTokenService)
        : ISessionService
    {
        private readonly ServiceConfiguration _configuration = options.Value;

        public async Task<Session> AddAsync(Session session, CancellationToken cancellationToken)
        {
            await sessionsRepository.AddAsync(session, cancellationToken);

            SessionCreated newSessionEvent = new(session);
            eventsRepository.AddNotification(newSessionEvent);

            await sessionsRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return session;
        }

        public async Task UpdateAsync(Session session, string token, CancellationToken cancellationToken, string userAgent = "Unknown")
        {
            session.Token = token;
            session.UserAgent = userAgent;
            session.Updated = DateTime.UtcNow;

            await sessionsRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<Session>> GetActiveSessionsAsync(string identityId, CancellationToken cancellationToken)
        {
            return await sessionsRepository
                .AsQueryable()
                .Where(e => DateTime.UtcNow - e.Updated < TimeSpan.FromHours(_configuration.ExpireRefreshTokenHours)
                            && e.IsActive
                            && e.IdentityId == identityId)
                .ToListAsync(cancellationToken);
        }

        public async Task DeleteAsync(string id, CancellationToken cancellationToken)
        {
            Session session = await sessionsRepository.FindByIdRequiredAsync(id, cancellationToken);
            await sessionsRepository.DeleteAsync(session, cancellationToken);
            await sessionsRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task DeactivateAsync(string id, CancellationToken cancellationToken)
        {
            Session session = await sessionsRepository.FindByIdRequiredAsync(id, cancellationToken);

            if (session.Token == null)
            {
                throw new RootServiceException(HttpStatusCode.NotFound)
                    .AddMessages("Токен сессии не найден");
            }     

            JwtRefreshTokenDto tokenData = refreshTokenService.Read(session.Token);
            await accessTokenBlacklistService.AddAsync(tokenData.AccessTokenId, cancellationToken);

            session.IsActive = false;

            await sessionsRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
