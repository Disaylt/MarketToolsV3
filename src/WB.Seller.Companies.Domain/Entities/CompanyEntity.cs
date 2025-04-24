using WB.Seller.Companies.Domain.Enums;
using WB.Seller.Companies.Domain.Events;
using WB.Seller.Companies.Domain.Seed;

namespace WB.Seller.Companies.Domain.Entities
{
    public class CompanyEntity : Entity
    {
        public string Name { get; private set; }
        public string? Token { get; private set; }

        private readonly List<SubscriptionEntity> _subscriptions = [];
        public IReadOnlyCollection<SubscriptionEntity> Subscriptions => _subscriptions;
        public IReadOnlyCollection<UserEntity> Users { get; private set; } = new List<UserEntity>();

        protected CompanyEntity()
        {
            Name = null!;

            AddDomainEvent(new CompanyCreatedDomainEvent(this));
        }

        public CompanyEntity(string name, string? token) : this()
        {
            Name = name;
            Token = token;
        }

        public void AddSubscription(SubscriptionEntity subscription)
        {
            _subscriptions.Add(subscription);
        }
    }
}
