using WB.Seller.Features.Automation.PriceManager.Domain.Enum;

namespace WB.Seller.Features.Automation.PriceManager.Domain.Entities.Events;

public class StockEventEntity : EventEntity
{
    public int Quantity { get; set; }
    public QuantityConditionEnum Condition { get; set; }
}