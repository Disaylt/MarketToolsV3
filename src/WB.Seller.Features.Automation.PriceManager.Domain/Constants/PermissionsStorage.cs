using WB.Seller.Features.Automation.PriceManager.Domain.Models;

namespace WB.Seller.Features.Automation.PriceManager.Domain.Constants
{
    public static class PermissionsStorage
    {
        private const string Automation = "automation";
        private const string AutomationPriceMonitoring = $"{Automation}:pricemonitoring";

        public static IReadOnlyCollection<PermissionModel> Values =>
        [
            new PermissionModel
            {
                Path = Automation,
                ChildPermissions = [
                    new PermissionModel
                    {
                        Path = AutomationPriceMonitoring,
                    }
                ]
            }
        ];
    }
}
