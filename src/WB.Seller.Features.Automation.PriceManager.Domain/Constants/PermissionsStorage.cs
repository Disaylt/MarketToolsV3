using System.Xml.Schema;
using WB.Seller.Features.Automation.PriceManager.Domain.Models;

namespace WB.Seller.Features.Automation.PriceManager.Domain.Constants
{
    public class PermissionsStorage
    {
        public string Root => "automation:price_manager";

        public IReadOnlyCollection<string> Collection =>
        [
            Root
        ];
    }
}
