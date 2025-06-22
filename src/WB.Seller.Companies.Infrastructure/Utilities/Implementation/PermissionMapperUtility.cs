using Proto.Contract.Common.PermissionStore;
using System.Xml.Linq;
using WB.Seller.Companies.Application.Models;
using WB.Seller.Companies.Infrastructure.Utilities.Abstract;
using PermissionStatus = WB.Seller.Companies.Domain.Enums.PermissionStatus;

namespace WB.Seller.Companies.Infrastructure.Utilities.Implementation;

public class PermissionMapperUtility : IPermissionMapperUtility
{
    public IEnumerable<PermissionSettingNodeDto> MapPermissionSettingNodes(IEnumerable<PermissionSettingNode> childNodes)
    {
        return childNodes.Select(permission => new PermissionSettingNodeDto
        {
            Name = permission.Name,
            Status = (PermissionStatus)permission.Status,
            Path = permission.Path,
            ChildNodes = [.. MapPermissionSettingNodes(permission.Children)]
        });
    }
}