using WB.Seller.Companies.Domain.DomainEvents;
using WB.Seller.Companies.Domain.Seed;

namespace WB.Seller.Companies.Domain.Entities;

public class SubscriptionCodeEntity : Entity
{
    public string Code { get; private set; }

    public SubscriptionCodeEntity(string code)
    {
        Code = code;
        AddCreateEvent(code);
    }

    private void AddCreateEvent(string code)
    {
        CreateSubscriptionCodeDomainEvent createEvent = new()
        {
            Code = code
        };

        AddDomainEvent(createEvent);
    }
}