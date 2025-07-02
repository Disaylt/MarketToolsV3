using Proto.Contract.Common.PermissionStore;
using WB.Seller.Companies.Application.Models;

namespace WB.Seller.Companies.Infrastructure.Utilities.Abstract;

public interface IPermissionMapperUtility
{
    IEnumerable<PermissionSettingNodeDto> MapPermissionSettingNodes(IEnumerable<PermissionSettingNode> childNodes);
}