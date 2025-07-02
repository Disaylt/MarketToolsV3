using MarketToolsV3.PermissionStore.Application.Models;

namespace MarketToolsV3.PermissionStore.Application.Utilities.Abstract;

public interface IPermissionNodeService
{
    IEnumerable<PermissionNodeDto> CreateNodes(IEnumerable<string[]> values, int index);
}