using MediatR;
using WB.Seller.Companies.Domain.Entities;

namespace WB.Seller.Companies.Domain.DomainEvents;

public class CreateSubscriptionCodeDomainEvent : INotification
{
    public required SubscriptionCodeEntity Entity { get; set; }
}