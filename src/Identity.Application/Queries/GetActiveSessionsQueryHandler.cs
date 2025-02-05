using Identity.Application.Models;
using Identity.Application.Services.Abstract;
using Identity.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Identity.Application.Mappers.Abstract;

namespace Identity.Application.Queries
{
    public class GetActiveSessionsQueryHandler(ISessionService sessionService,
        ISessionMapper<SessionDto> sessionMapper)
        : IRequestHandler<GetActiveSessionsQuery, IEnumerable<SessionDto>>
    {
        public async Task<IEnumerable<SessionDto>> Handle(GetActiveSessionsQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Session> sessions = await sessionService.GetActiveSessionsAsync(request.UserId, cancellationToken);

            return sessions
                .Select(sessionMapper.Map)
                .ToList();
        }
    }
}
