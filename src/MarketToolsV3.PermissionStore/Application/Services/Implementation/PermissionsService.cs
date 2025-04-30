using MarketToolsV3.PermissionStore.Application.Models;
using MarketToolsV3.PermissionStore.Application.Services.Abstract;
using MarketToolsV3.PermissionStore.Domain.Entities;
using MediatR;

namespace MarketToolsV3.PermissionStore.Application.Services.Implementation;

public class PermissionsService : IPermissionsService
{
    public ActionsStoreModel<PermissionEntity> DistributeByActions(
        Dictionary<string, PermissionEntity> pathAndExistsPermissionPairs, 
        IReadOnlyCollection<ModulePermissionDto> permissions, 
        string module)
    {
        ActionsStoreModel<PermissionEntity> options = new();

        foreach (var requestPermission in permissions)
        {
            PermissionEntity? permission = pathAndExistsPermissionPairs.GetValueOrDefault(requestPermission.Path);

            if (permission is null)
            {
                PermissionEntity newPermission = new()
                {
                    Module = module,
                    Path = requestPermission.Path,
                    ViewName = requestPermission.ViewName
                };
                options.ToAdd.Add(newPermission);
                continue;
            }

            if (permission.ViewName != requestPermission.ViewName)
            {
                permission.ViewName = requestPermission.ViewName;
                options.ToUpdate.Add(permission);
            }

            pathAndExistsPermissionPairs.Remove(requestPermission.Path);
        }

        options.ToRemove.AddRange(pathAndExistsPermissionPairs.Values);

        return options;
    }
}