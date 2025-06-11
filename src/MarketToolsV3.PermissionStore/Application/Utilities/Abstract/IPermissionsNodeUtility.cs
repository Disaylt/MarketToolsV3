using MarketToolsV3.PermissionStore.Application.Models;

namespace MarketToolsV3.PermissionStore.Application.Utilities.Abstract;

public interface IPermissionsNodeUtility
{
    IDictionary<string, IEnumerable<PermissionSlimNodeDto>> GetChildPermissions(
        IEnumerable<string> permissions,
        string parentSegment);
}