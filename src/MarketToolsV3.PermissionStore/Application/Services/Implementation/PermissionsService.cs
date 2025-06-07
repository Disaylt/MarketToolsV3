using MarketToolsV3.PermissionStore.Application.Models;
using MarketToolsV3.PermissionStore.Application.Services.Abstract;
using MarketToolsV3.PermissionStore.Domain.Entities;
using MediatR;

namespace MarketToolsV3.PermissionStore.Application.Services.Implementation;

public class PermissionsService : IPermissionsService
{
    public ActionsStoreModel<PermissionEntity> DistributeByActions(
        IEnumerable<PermissionEntity> existsPermissions,
        IEnumerable<string> permissions, 
        string module)
    {
        ActionsStoreModel<PermissionEntity> options = new();

        Dictionary<string, PermissionEntity> pathAndEntityPairs = existsPermissions
            .ToDictionary(x => x.Path);

        foreach (var requestPermission in permissions)
        {
            PermissionEntity? permission = pathAndEntityPairs.GetValueOrDefault(requestPermission);

            if (permission is null)
            {
                PermissionEntity newPermission = new()
                {
                    Path = requestPermission,
                    Module = module
                };
                options.ToAdd.Add(newPermission);
                continue;
            }

            pathAndEntityPairs.Remove(requestPermission);
        }

        options.ToRemove.AddRange(pathAndEntityPairs.Values);

        return options;
    }
}