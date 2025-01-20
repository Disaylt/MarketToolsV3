using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WB.Seller.Companies.Domain.Seed;

namespace WB.Seller.Companies.Domain.Entities
{
    public class Subscriber : Entity
    {
        public string SubId { get; private set; }
        public string Login { get; private set; }

        private readonly List<Subscription> _subscriptions = new();
        public IReadOnlyCollection<Subscription> Subscriptions => _subscriptions;

        private readonly List<Company> _companies = new();
        public IReadOnlyCollection<Company> Companies => _companies;

        protected Subscriber()
        {
            SubId = null!;
            Login = null!;
        }

        public Subscriber(string id, string login) : this()
        {
            SubId = id;
            Login = login;
        }
    }
}
