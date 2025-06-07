using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketToolsV3.PermissionStore.Application.Models;
using MarketToolsV3.PermissionStore.Domain.Entities;

namespace MarketToolsV3.PermissionStore.Application.Services.Abstract
{
    public interface IPermissionsService
    {
        ActionsStoreModel<PermissionEntity> DistributeByActions(
            IEnumerable<PermissionEntity> existsPermissions,
            IEnumerable<string> permissions,
            string module);
    }
}
