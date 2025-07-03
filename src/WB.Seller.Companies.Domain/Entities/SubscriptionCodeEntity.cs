using WB.Seller.Companies.Domain.DomainEvents;
using WB.Seller.Companies.Domain.Seed;

namespace WB.Seller.Companies.Domain.Entities;

public class SubscriptionCodeEntity : Entity
{
    public string Code { get; private set; }

    public int SubscriptionId { get; private set; }
    public SubscriptionEntity Subscription { get; private set; }

    public SubscriptionCodeEntity(string code)
    {
        Code = code;
        AddCreateEvent();
    }

    public SubscriptionCodeEntity(string code, SubscriptionEntity? subscription = null, int? subscriptionId = null)
    : this(code)
    {
        Subscription = subscription ?? null!;
        SubscriptionId = subscriptionId ?? 0;
    }

    private void AddCreateEvent()
    {
        CreateSubscriptionCodeDomainEvent createEvent = new()
        {
            Entity = this
        };

        AddDomainEvent(createEvent);
    }
}