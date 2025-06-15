using WB.Seller.Features.Automation.PriceManager.Domain.Seed;

namespace WB.Seller.Features.Automation.PriceManager.Domain.Entities;

public class CardEntity : Entity
{
    public required string Name { get; set; }
    public string? UrlPhoto { get; set; }
    public int WebCardId { get; set; }

    public IReadOnlyCollection<TriggerEntity> Triggers { get; private set; } = [];
}