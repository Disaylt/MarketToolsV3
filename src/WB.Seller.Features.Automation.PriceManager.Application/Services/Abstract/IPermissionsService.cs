using WB.Seller.Features.Automation.PriceManager.Application.Models;

namespace WB.Seller.Features.Automation.PriceManager.Application.Services.Abstract;

public interface IPermissionsService
{
    IEnumerable<PermissionInfoDto> GetPermissions();
}