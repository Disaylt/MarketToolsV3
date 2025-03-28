using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Identity.Domain.Entities;
using Identity.Domain.Seed;
using MediatR;

namespace Identity.Application.Commands
{
    public class CreateModuleCommandHandler(
        IRepository<Module> moduleRepository)
     : IRequestHandler<CreateModuleCommand, Unit>
    {
        public async Task<Unit> Handle(CreateModuleCommand request, CancellationToken cancellationToken)
        {
            Module module = new Module
            {
                ExternalId = request.Id,
                IdentityId = request.UserId,
                Path = request.Path,
                Roles = request.Roles
                    .Select(x => new ModuleRole
                    {
                        Value = x
                    })
                    .ToList(),
                Claims = request.Claims
                    .Select(x => new ModuleClaim
                    {
                        Type = x.Key,
                        Value = x.Value
                    })
                    .ToList()
            };
            await moduleRepository.AddAsync(module, cancellationToken);
            await moduleRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
