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
using Identity.Application.QueryObjects;
using Identity.Domain.Seed;
using Microsoft.Extensions.Options;

namespace Identity.Application.Queries
{
    public class GetActiveSessionsQueryHandler(IOptions<ServiceConfiguration> options,
        IQueryObjectHandler<GetActivateSessionQueryObject, Session> sessionQueryObjectAdapter,
        IQueryableHandler<Session, SessionDto> sessionToTransferHandler,
        IExtensionRepository extensionRepository)
        : IRequestHandler<GetActiveSessionsQuery, IEnumerable<SessionDto>>
    {
        public async Task<IEnumerable<SessionDto>> Handle(GetActiveSessionsQuery request, CancellationToken cancellationToken)
        {
            GetActivateSessionQueryObject queryObject = new()
            {
                IdentityId = request.UserId,
                ExpireRefreshTokenHours = options.Value.ExpireRefreshTokenHours
            };

            IQueryable<Session> sessionQuery = sessionQueryObjectAdapter.Create(queryObject);
            IQueryable<SessionDto> sessionTransferQuery = await sessionToTransferHandler.HandleAsync(sessionQuery);

            return await extensionRepository.ToListAsync(sessionTransferQuery);
        }
    }
}
