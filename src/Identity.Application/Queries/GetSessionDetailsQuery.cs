using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Identity.Application.Models;
using Identity.Domain.Entities;
using Identity.Domain.Seed;
using MediatR;
using Microsoft.Extensions.Options;

namespace Identity.Application.Queries
{
    public class GetSessionDetailsQuery : IRequest<SessionStatusDto>
    {
        public required string Id { get; set; }
    }


    public class GetSessionDetailsQueryHandler(ICacheRepository<SessionStatusDto> sessionCacheRepository,
        IRepository<Session> sessionRepository,
        IOptions<ServiceConfiguration> options)
        : IRequestHandler<GetSessionDetailsQuery, SessionStatusDto>
    {
        public async Task<SessionStatusDto> Handle(GetSessionDetailsQuery request, CancellationToken cancellationToken)
        {
            SessionStatusDto? sessionStatus = await sessionCacheRepository.GetAsync(request.Id);

            if (sessionStatus != null) return sessionStatus;

            Session entity = await sessionRepository.FindByIdRequiredAsync(request.Id, cancellationToken);
            SessionStatusDto newSessionStatus = new() { Id = entity.Id, IsActive = entity.IsActive };

            await sessionCacheRepository.SetAsync(newSessionStatus.Id,
            newSessionStatus,
                TimeSpan.FromHours(options.Value.ExpireRefreshTokenHours));

            return newSessionStatus;
        }
    }
}
