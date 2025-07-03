using MediatR;

namespace WB.Seller.Companies.Domain.DomainEvents;

public class CreateSubscriptionCodeDomainEvent : INotification
{
    public required string Code { get; set; }
}