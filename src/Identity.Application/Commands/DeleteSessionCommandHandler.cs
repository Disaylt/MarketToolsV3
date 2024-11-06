using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Identity.Application.Models;
using Identity.Application.Services;
using Identity.Domain.Seed;
using MediatR;

namespace Identity.Application.Commands
{
    public class DeleteSessionCommandHandler(ICacheRepository<SessionStatusDto> sessionCacheRepository,
        ISessionService sessionService)
        : IRequestHandler<DeleteSessionCommand, Unit>
    {
        public async Task<Unit> Handle(DeleteSessionCommand request, CancellationToken cancellationToken)
        {
            await sessionService.DeleteAsync(request.Id, cancellationToken);
            await sessionCacheRepository.DeleteAsync(request.Id, cancellationToken);

            return Unit.Value;
        }
    }
}
