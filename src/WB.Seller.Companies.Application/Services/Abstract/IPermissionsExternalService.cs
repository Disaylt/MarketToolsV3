using WB.Seller.Companies.Application.Models;

namespace WB.Seller.Companies.Application.Services.Abstract;

public interface IPermissionsExternalService
{
    Task<IEnumerable<PermissionSettingNodeDto>> GetPermissionsSettingTreeAsync(IEnumerable<PermissionDto> userPermissions);
}