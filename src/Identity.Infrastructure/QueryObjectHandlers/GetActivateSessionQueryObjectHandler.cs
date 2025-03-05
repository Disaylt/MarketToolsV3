using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Identity.Application.QueryObjects;
using Identity.Domain.Entities;
using Identity.Domain.Seed;
using Microsoft.Extensions.Configuration;

namespace Identity.Infrastructure.QueryObjectAdapters
{
    internal class GetActivateSessionQueryObjectHandler(IRepository<Session> sessionRepository)
        : IQueryObjectHandler<GetActivateSessionQueryObject, Session>
    {
        public IQueryable<Session> Create(GetActivateSessionQueryObject query)
        {
            TimeSpan expireTime = TimeSpan.FromHours(query.ExpireRefreshTokenHours);

            return sessionRepository
                .AsQueryable()
                .Where(e => e.IsActive
                            && DateTime.UtcNow - e.Updated < expireTime
                            && e.IdentityId == query.IdentityId);
        }
    }
}
