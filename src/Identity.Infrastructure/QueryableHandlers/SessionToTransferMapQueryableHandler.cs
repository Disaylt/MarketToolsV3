using Identity.Application.Models;
using Identity.Domain.Seed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Identity.Domain.Entities;

namespace Identity.Infrastructure.QueryableHandlers
{
    public class SessionToTransferMapQueryableHandler : IQueryableHandler<Session, SessionDto>
    {
        public Task<IQueryable<SessionDto>> HandleAsync(IQueryable<Session> query)
        {
            var result = query
                .Select(x => new SessionDto
                {
                    CreateDate = x.Created,
                    Id = x.Id,
                    IsActive = x.IsActive,
                    Updated = x.Updated,
                    UserAgent = x.UserAgent,
                    UserId = x.IdentityId
                });

            return Task.FromResult(result);
        }
    }
}
