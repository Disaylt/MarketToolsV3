﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Identity.Application.Mappers.Abstract;
using Identity.Application.Models;
using Identity.Application.Services.Abstract;
using Identity.Domain.Entities;
using Identity.Domain.Seed;

namespace Identity.Application.Services.Implementation
{
    internal class SessionQuickSearchService(
        ICacheRepository sessionCacheRepository,
        IRepository<Session> sessionRepository,
        ISessionMapper<SessionDto> sessionMapper)
        : IStringIdQuickSearchService<SessionDto>
    {
        public async Task ClearAsync(string id, CancellationToken cancellationToken)
        {
            await sessionCacheRepository.DeleteAsync<SessionDto>(id, cancellationToken);
        }

        public async Task<SessionDto> GetAsync(string id, TimeSpan expire, CancellationToken cancellationToken)
        {
            SessionDto? session = await sessionCacheRepository.GetAsync<SessionDto>(id, cancellationToken);

            if (session != null) return session;

            Session entity = await sessionRepository.FindByIdRequiredAsync(id, cancellationToken);

            session = sessionMapper.Map(entity);

            await sessionCacheRepository.SetAsync(session.Id, session, expire, cancellationToken);

            return session;
        }
    }
}
