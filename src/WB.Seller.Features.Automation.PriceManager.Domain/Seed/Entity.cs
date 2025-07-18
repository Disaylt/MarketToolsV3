﻿using MediatR;

namespace WB.Seller.Features.Automation.PriceManager.Domain.Seed
{
    public abstract class Entity
    {
        public virtual int Id { get; protected set; }
        public DateTime Created { get; private set; } = DateTime.UtcNow;
        public DateTime Updated { get; protected set; }

        private readonly List<INotification> _domainEvents = [];
        public IReadOnlyCollection<INotification> DomainEvents => _domainEvents.AsReadOnly();

        public void AddDomainEvent(INotification eventItem)
        {
            _domainEvents.Add(eventItem);
        }

        public void RemoveDomainEvent(INotification eventItem)
        {
            _domainEvents.Remove(eventItem);
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
    }
}
