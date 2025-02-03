using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Identity.Application.Models;
using Identity.Application.Utilities.Sessions.Abstract;
using Identity.Domain.Entities;
using Identity.Domain.Seed;

namespace Identity.Application.Utilities.Sessions.Implementation
{
    internal class SessionDtoSearchBuilder(
        ICacheRepository<SessionDto> sessionCacheRepository,
        IRepository<Session> sessionRepository)
        : ISessionSearchBuilder<SessionDto>
    {
        private TimeSpan? _cacheTime;
        private bool _useCache;
        private bool _useDatabase;

        public async Task<SessionDto> SearchAsync(string id)
        {
            SessionDto? sessionStatus = null;

            if (_useCache)
            {
                sessionStatus = await sessionCacheRepository.GetAsync(id);
            }

            if (sessionStatus != null) return sessionStatus;

            Session entity = await sessionRepository.FindByIdRequiredAsync(request.Id, cancellationToken);
            SessionStatusDto newSessionStatus = new() { Id = entity.Id, IsActive = entity.IsActive };

            await sessionCacheRepository.SetAsync(newSessionStatus.Id,
                newSessionStatus,
                TimeSpan.FromHours(options.Value.ExpireRefreshTokenHours));

            return newSessionStatus;
        }

        public ISessionSearchBuilder<SessionDto> SetCacheTime(TimeSpan timeSpan)
        {
            _cacheTime = timeSpan;

            return this;
        }

        public ISessionSearchBuilder<SessionDto> UseCache()
        {
            _useCache = true;

            return this;
        }

        public ISessionSearchBuilder<SessionDto> UseDatabase()
        {
            _useDatabase = true;

            return this;
        }
    }
}
