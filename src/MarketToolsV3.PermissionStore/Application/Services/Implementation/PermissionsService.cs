using MarketToolsV3.PermissionStore.Application.Models;
using MarketToolsV3.PermissionStore.Application.Services.Abstract;
using MarketToolsV3.PermissionStore.Domain.Entities;
using MediatR;

namespace MarketToolsV3.PermissionStore.Application.Services.Implementation;

public class PermissionsService : IPermissionsService
{
    public ActionsStoreModel<ModuleEntity> DistributeByActions(
        ModuleEntity module,
        IEnumerable<string> permissions)
    {
        ActionsStoreModel<ModuleEntity> options = new();

        Dictionary<string, ModuleEntity> pathAndEntityPairs = existsPermissions
            .ToDictionary(x => x.Path);

        foreach (var requestPermission in permissions)
        {
            ModuleEntity? permission = pathAndEntityPairs.GetValueOrDefault(requestPermission);

            if (permission is null)
            {
                ModuleEntity newPermission = new()
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