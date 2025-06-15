namespace WB.Seller.Features.Automation.PriceManager.Domain.Entities.Actions;

public class SinglePriceActionEntity : ActionEntity
{
    public int? Price { get; set; }
    public int? Discount { get; set; }
}