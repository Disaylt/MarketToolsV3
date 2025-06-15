using WB.Seller.Features.Automation.PriceManager.Domain.Enum;

namespace WB.Seller.Features.Automation.PriceManager.Domain.Entities.Events;

public class CardPriceEventEntity : EventEntity
{
    public int Article { get; set; }
    public QuantityConditionEnum Condition { get; set; }
}