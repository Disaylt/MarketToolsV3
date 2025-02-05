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
        public Task ClearAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<SessionDto> GetAsync(string id, TimeSpan expire)
        {
            SessionDto? sessionStatus = await sessionCacheRepository.GetAsync(id);

            if (sessionStatus != null) return sessionStatus;

            Session entity = await sessionRepository.FindByIdRequiredAsync(id, CancellationToken.None);
            SessionDto newSessionStatus = new() { Id = entity.Id, IsActive = entity.IsActive };

            await sessionCacheRepository.SetAsync(newSessionStatus.Id, expire));

            return newSessionStatus;
        }
    }
}
