namespace WB.Seller.Features.Automation.PriceManager.Domain.Entities.Events
{
    public class TimeRangeEventEntity : EventEntity
    {
        public required DateTime From { get; set; }
        public required DateTime To { get; set; }
    }
}
