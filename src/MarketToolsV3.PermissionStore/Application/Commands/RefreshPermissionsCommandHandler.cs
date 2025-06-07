using MarketToolsV3.PermissionStore.Application.Models;
using MarketToolsV3.PermissionStore.Application.Services.Abstract;
using MarketToolsV3.PermissionStore.Domain.Entities;
using MarketToolsV3.PermissionStore.Domain.Seed;
using MediatR;

namespace MarketToolsV3.PermissionStore.Application.Commands;

public class RefreshPermissionsCommandHandler(
    IPermissionsService permissionsService,
    IRepository<PermissionEntity> permissionsRepository,
    IExtensionRepository extensionRepository)
    : IRequestHandler<RefreshPermissionsCommand, Unit>
{
    
    public async Task<Unit> Handle(RefreshPermissionsCommand request, CancellationToken cancellationToken)
    {
        IQueryable<PermissionEntity> existsPermissionsQuery = permissionsRepository
            .AsQueryable()
            .Where(x => x.Module == request.Module);

        IReadOnlyCollection<PermissionEntity> existsPermissions =
            await extensionRepository.ToListAsync(existsPermissionsQuery, cancellationToken);

        ActionsStoreModel<PermissionEntity> options = permissionsService
            .DistributeByActions(existsPermissions, request.Permissions, request.Module);

        Task[] tasks =
        [
            permissionsRepository.InsertManyAsync(options.ToAdd, cancellationToken),
            permissionsRepository.RemoveRangeAsync(options.ToRemove, cancellationToken)
        ];

        await Task.WhenAll(tasks);

        return Unit.Value;
    }
}