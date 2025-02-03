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

namespace Identity.Application.Commands
{
    public class DeactivateSessionCommandHandler(ICacheRepository<SessionStatusDto> sessionCacheRepository,
        ISessionService sessionService)
        : IRequestHandler<DeactivateSessionCommand, Unit>
    {
        public async Task<Unit> Handle(DeactivateSessionCommand request, CancellationToken cancellationToken)
        {
            await sessionService.DeactivateAsync(request.Id, cancellationToken);
            await sessionCacheRepository.DeleteAsync(request.Id, cancellationToken);

            return Unit.Value;
        }
    }
}
