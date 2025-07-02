using MarketToolsV3.PermissionStore.Application.Models;
using MarketToolsV3.PermissionStore.Application.Utilities.Abstract;
using System.Xml.Linq;

namespace MarketToolsV3.PermissionStore.Application.Utilities.Implementation;

public class PermissionNodeService : IPermissionNodeService
{
    public IEnumerable<PermissionNodeDto> CreateNodes(IEnumerable<string[]> values, int index)
    {
        return values
            .Where(v => v.Length > index)
            .GroupBy(v => v[index])
            .Select(g => new PermissionNodeDto(g.Key)
            {
                Next = CreateNodes(g, index + 1)
            });
    }
}