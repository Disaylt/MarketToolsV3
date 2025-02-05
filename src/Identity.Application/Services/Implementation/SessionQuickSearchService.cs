using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Identity.Application.Models;
using Identity.Application.Services.Abstract;
using Identity.Domain.Entities;
using Identity.Domain.Seed;

namespace Identity.Application.Services.Implementation
{
    internal class SessionQuickSearchService(
        ICacheRepository<SessionDto> sessionCacheRepository,
        IRepository<Session> sessionRepository)
        : IStringIdQuickSearchModel<SessionDto>
    {
        public async Task ClearAsync(string id)
        {
            await sessionCacheRepository.DeleteAsync(id, CancellationToken.None);
        }

        public async Task<SessionDto> GetAsync(string id, TimeSpan expire)
        {
            SessionDto? session = await sessionCacheRepository.GetAsync(id);

            if (session != null) return session;

            Session entity = await sessionRepository.FindByIdRequiredAsync(id, CancellationToken.None);

            session = new SessionDto
            {
                CreateDate = entity.Created,
                Updated = entity.Updated,
                Id = entity.Id,
                IsActive = entity.IsActive,
                UserAgent = entity.UserAgent
            };

            await sessionCacheRepository.SetAsync(session.Id, session, expire);

            return session;
        }
    }
}
