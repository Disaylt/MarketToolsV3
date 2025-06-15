using WB.Seller.Features.Automation.PriceManager.Domain.Entities.Actions;
using WB.Seller.Features.Automation.PriceManager.Domain.Entities.Events;
using WB.Seller.Features.Automation.PriceManager.Domain.Seed;

namespace WB.Seller.Features.Automation.PriceManager.Domain.Entities
{
    public class TriggerEntity : Entity
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }

        public int CompanyId { get; init; }

        public IReadOnlyCollection<CardEntity> Cards { get; private set; } = [];
        public IReadOnlyCollection<EventEntity> Events { get; private set; } = [];
        public IReadOnlyCollection<ActionEntity> Actions { get; private set; } = [];
    }
}
