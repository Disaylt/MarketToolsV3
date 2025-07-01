using MarketToolsV3.ConfigurationManager.Models;
using Microsoft.Extensions.Options;
using WB.Seller.Features.Automation.PriceManager.Application.Models;
using WB.Seller.Features.Automation.PriceManager.Application.Seed;
using WB.Seller.Features.Automation.PriceManager.Application.Services.Abstract;

namespace WB.Seller.Features.Automation.PriceManager.Infrastructure.Services.Implementation;

public class PermissionsService(IOptions<ServicesAddressesConfig> addressesOptions) 
    : IPermissionsService
{
    public IEnumerable<PermissionInfoDto> GetPermissions()
    {
        return [
            new PermissionInfoDto
            {
                Path = PermissionsStorage.Root,
                RequireUse = true,
                AvailableModules = [
                    GetWbCompanyModule()
                    ]
            }
        ];
    }

    private string GetWbCompanyModule()
    {
        return addressesOptions
            .Value
            .Wb
            .Seller
            .Companies
            .Name;
    }
}