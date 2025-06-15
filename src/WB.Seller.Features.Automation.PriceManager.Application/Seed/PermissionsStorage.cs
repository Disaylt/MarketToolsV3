using System.Xml.Schema;

namespace WB.Seller.Features.Automation.PriceManager.Application.Seed
{
    public sealed class PermissionsStorage
    {
        public static string Root => "automation:price_manager";

        public static IReadOnlyCollection<string> Collection =>
        [
            Root
        ];
    }
}
