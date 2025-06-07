namespace WB.Seller.Features.Automation.PriceManager.Domain.Entities.Actions;

public class RangePriceActionEntity : ActionEntity
{
    public int? PriceMin { get; set; }
    public int? PriceMax { get; set; }

    public int? DiscountMin { get; set; }
    public int? DiscountMax { get; set; }
}