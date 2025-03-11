using WB.Seller.Companies.Domain.Enums;
using WB.Seller.Companies.Domain.Seed;

namespace WB.Seller.Companies.Domain.Entities
{
    public class CompanyEntity : Entity
    {
        public string Name { get; private set; }
        public string? Token { get; private set; }
        public bool IsActive { get; private set; }
        public CompanyState State { get; private set; }
        public int NumberOfTokenChecks { get; private set; }
        public DateTime StateUpdated { get; private set; }

        private readonly List<SubscriptionEntity> _subscriptions = new();
        public IReadOnlyCollection<SubscriptionEntity> Subscriptions => _subscriptions;

        private readonly List<UserEntity> _users = new();
        public IReadOnlyCollection<UserEntity> Users => _users;

        protected CompanyEntity()
        {
            Name = null!;
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

        public void AddUser(UserEntity user)
        {
            _users.Add(user);
        }
    }
}
