using Identity.Application.Models;
using Identity.Application.Services;
using Identity.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Queries
{
    public class GetActiveSessionsQueryHandler(ISessionService sessionService)
        : IRequestHandler<GetActiveSessionsQuery, IEnumerable<SessionDto>>
    {
        public async Task<IEnumerable<SessionDto>> Handle(GetActiveSessionsQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Session> sessions = await sessionService.GetActiveSessionsAsync(request.UserId, cancellationToken);

            return sessions
                .Select(x => new SessionDto
                {
                    CreateDate = x.Created,
                    Id = x.Id,
                    IsCurrent = request.CurrentSessionId == x.Id,
                    Updated = x.Updated,
                    UserAgent = x.UserAgent
                })
                .ToList();
        }
    }
}
