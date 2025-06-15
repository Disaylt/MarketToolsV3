using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WB.Seller.Features.Automation.PriceMonitoring.Domain.Models;

namespace WB.Seller.Features.Automation.PriceMonitoring.Domain.Constants
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
