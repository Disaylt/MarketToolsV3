using WB.Seller.Features.Automation.PriceManager.Domain.Enum;

namespace WB.Seller.Features.Automation.PriceManager.Domain.Entities.Actions;

public class ChangePriceActionEntity : ActionEntity
{
    public int? Discount { get; set; }
    public int? Price { get; set; }
    public MathActionEnum PriceAction { get; set; }
    public MathActionEnum DiscountAction { get; set; }

    public MathValueEnum PriceMathValue { get; set; }
    public MathValueEnum DiscountMathValue { get; set; }
}