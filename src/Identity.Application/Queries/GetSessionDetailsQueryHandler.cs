using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Identity.Application.Models;
using Identity.Application.Services.Abstract;
using Identity.Domain.Entities;
using Identity.Domain.Seed;
using MediatR;
using Microsoft.Extensions.Options;

namespace Identity.Application.Queries
{
    public class GetSessionDetailsQueryHandler(
        IStringIdQuickSearchModel<SessionDto> sessionSearchService,
        IOptions<ServiceConfiguration> options)
        : IRequestHandler<GetSessionDetailsQuery, SessionStatusDto>
    {
        public async Task<SessionStatusDto> Handle(GetSessionDetailsQuery request, CancellationToken cancellationToken)
        {
            TimeSpan expireSessionTime = TimeSpan.FromHours(options.Value.ExpireRefreshTokenHours);
            SessionDto session = await sessionSearchService.GetAsync(request.Id, expireSessionTime);

            return new SessionStatusDto
            {
                Id = session.Id,
                IsActive = session.IsActive
            };
        }
    }
}
