using WB.Seller.Features.Automation.PriceMonitoring.Domain.Enum;

namespace WB.Seller.Features.Automation.PriceMonitoring.Domain.Entities.Events;

public class CardPriceEventEntity : EventEntity
{
    public int Article { get; set; }
    public QuantityConditionEnum Condition { get; set; }
}