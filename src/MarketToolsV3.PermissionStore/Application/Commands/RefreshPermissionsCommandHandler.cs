using MarketToolsV3.PermissionStore.Application.Models;
using MarketToolsV3.PermissionStore.Application.Services.Abstract;
using MarketToolsV3.PermissionStore.Domain.Entities;
using MarketToolsV3.PermissionStore.Domain.Seed;
using MediatR;

namespace MarketToolsV3.PermissionStore.Application.Commands;

public class RefreshPermissionsCommandHandler(
    IRepository<ModuleEntity> moduleRepository,
    IExtensionRepository extensionRepository)
    : IRequestHandler<RefreshPermissionsCommand, Unit>
{
    
    public async Task<Unit> Handle(RefreshPermissionsCommand request, CancellationToken cancellationToken)
    {
        IQueryable<ModuleEntity> existsPermissionsQuery = moduleRepository
            .AsQueryable()
            .Where(x => x.Module == request.Module);

        ModuleEntity? module = await extensionRepository.FirstOrDefaultAsync(existsPermissionsQuery, cancellationToken);

        if (module is null)
        {
            ModuleEntity newModule = new()
            {
                Module = request.Module,
                ParentModules = [.. request.ParentModules],
                Permissions = [.. request.Permissions]
            };

            await moduleRepository.InsertAsync(newModule, cancellationToken);
        }
        else
        {
            module.ParentModules = [.. request.ParentModules];
            module.Permissions = [.. request.Permissions];

            await moduleRepository.UpdateAsync(module, cancellationToken);
        }

        return Unit.Value;
    }
}