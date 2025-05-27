using MarketToolsV3.PermissionStore.Application.Models;
using MarketToolsV3.PermissionStore.Application.Services.Abstract;
using MarketToolsV3.PermissionStore.Domain.Entities;
using MarketToolsV3.PermissionStore.Domain.Seed;
using MediatR;

namespace MarketToolsV3.PermissionStore.Application.Commands;

public class RefreshPermissionsCommandHandler(
    IPermissionsEntityService permissionsEntityService,
    IPermissionsService permissionsService,
    IRepository<PermissionEntity> permissionsRepository)
    : IRequestHandler<RefreshPermissionsCommand, Unit>
{
    
    public async Task<Unit> Handle(RefreshPermissionsCommand request, CancellationToken cancellationToken)
    {
        Dictionary<string, PermissionEntity> pathAndExistsPermissionPairs = await permissionsEntityService
            .GetRangeByModuleAsync(request.Module, cancellationToken)
            .ContinueWith(x => x.Result.ToDictionary(item => item.Path), cancellationToken);

        ActionsStoreModel<PermissionEntity> options = permissionsService
            .DistributeByActions(pathAndExistsPermissionPairs, request.Permissions, request.Module);

        Task[] tasks =
        [
            permissionsRepository.InsertManyAsync(options.ToAdd, cancellationToken),
            permissionsRepository.RemoveRangeAsync(options.ToRemove, cancellationToken),
            permissionsRepository.UpdateRangeAsync(options.ToUpdate, cancellationToken)
        ];

        await Task.WhenAll(tasks);

        return Unit.Value;
    }
}