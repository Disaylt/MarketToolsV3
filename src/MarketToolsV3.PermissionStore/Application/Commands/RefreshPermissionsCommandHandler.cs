using MarketToolsV3.PermissionStore.Application.Models;
using MarketToolsV3.PermissionStore.Application.Services.Abstract;
using MarketToolsV3.PermissionStore.Domain.Entities;
using MarketToolsV3.PermissionStore.Domain.Seed;
using MarketToolsV3.PermissionStore.Domain.ValueObjects;
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
            .Where(x => x.Creator == request.Creator);

        ModuleEntity? module = await extensionRepository.FirstOrDefaultAsync(existsPermissionsQuery, cancellationToken);

        if (module is null)
        {
            ModuleEntity newModule = new()
            {
                Creator = request.Creator,
                Permissions = MapPermissions(request.Permissions)
            };

            await moduleRepository.InsertAsync(newModule, cancellationToken);
        }
        else
        {
            module.Permissions = MapPermissions(request.Permissions);
            await moduleRepository.UpdateAsync(module, cancellationToken);
        }

        return Unit.Value;
    }

    private IEnumerable<PermissionValueObject> MapPermissions(IEnumerable<PermissionDto> permissions)
    {
        return permissions
            .Select(p => new PermissionValueObject
            {
                AvailableModules = p.AvailableModules,
                Path = p.Path,
                RequireUse = p.RequireUse
            });
    }
}