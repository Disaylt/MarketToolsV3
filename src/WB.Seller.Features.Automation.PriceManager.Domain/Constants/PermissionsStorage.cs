using System.Xml.Schema;
using WB.Seller.Features.Automation.PriceManager.Domain.Models;

namespace WB.Seller.Features.Automation.PriceManager.Domain.Constants
{
    public static class PermissionsStorage
    {
        public const string Root = "wb:seller:automation:price_monitoring";

        public static IReadOnlyCollection<string> Values =>
        [
            Root
        ];
    }
}
