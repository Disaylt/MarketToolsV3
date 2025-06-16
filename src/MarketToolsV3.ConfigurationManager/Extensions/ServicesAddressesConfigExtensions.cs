using MarketToolsV3.ConfigurationManager.Models;

namespace MarketToolsV3.ConfigurationManager.Extensions;

public static class ServicesAddressesConfigExtensions
{
    public static ModuleConfig GetPermissionsModule(this ServicesAddressesConfig config)
    {
        return config
            .Common
            .Permissions;
    }

    public static ModuleConfig GetApiGatewayModule(this ServicesAddressesConfig config)
    {
        return config.ApiGateway;
    }

    public static ModuleConfig GetIdentityModule(this ServicesAddressesConfig config)
    {
        return config.Identity;
    }

    public static ModuleConfig GetWbSellerCompaniesModule(this ServicesAddressesConfig config)
    {
        return config.Wb.Seller.Companies;
    }
}