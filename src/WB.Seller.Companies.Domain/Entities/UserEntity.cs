using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WB.Seller.Companies.Domain.Seed;

namespace WB.Seller.Companies.Domain.Entities
{
    public class UserEntity : Entity
    {
        public string SubId { get; private set; }
        public string Login { get; private set; }

        private readonly List<SubscriptionEntity> _subscriptions = [];
        public IReadOnlyCollection<SubscriptionEntity> Subscriptions => _subscriptions;

        private readonly List<CompanyEntity> _companies = [];
        public IReadOnlyCollection<CompanyEntity> Companies => _companies;

        protected UserEntity()
        {
            SubId = null!;
            Login = null!;
        }

        public UserEntity(string id, string login) : this()
        {
            SubId = id;
            Login = login;
        }

        public void AddSubscription(SubscriptionEntity subscription)
        {
            _subscriptions.Add(subscription);
        }

        public void AddCompanies(CompanyEntity company)
        {
            _companies.Add(company);
        }
    }
}
