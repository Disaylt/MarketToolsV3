using MarketToolsV3.PermissionStore.Application.Models;
using MarketToolsV3.PermissionStore.Application.Utilities.Abstract;

namespace MarketToolsV3.PermissionStore.Application.Utilities.Implementation;

public class PermissionsNodeUtility : IPermissionsNodeUtility
{
    public IDictionary<string, IEnumerable<PermissionSlimNodeDto>> GetChildPermissions(IEnumerable<string> permissions, string parentSegment)
    {
        return permissions
            .Where(p => p.StartsWith(parentSegment + ":"))
            .Select(p =>
            {
                var segments = p.Split(':');
                return new PermissionSlimNodeDto
                {
                    NextSegment = p.Split(':')[Array.IndexOf(segments, parentSegment) + 1],
                    Permission = p
                };
            })
            .GroupBy(x => x.NextSegment)
            .ToDictionary(
                x => x.Key,
                x => x.AsEnumerable());
    }
}