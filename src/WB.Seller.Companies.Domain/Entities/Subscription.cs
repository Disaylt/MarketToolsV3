using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WB.Seller.Companies.Domain.Seed;

namespace WB.Seller.Companies.Domain.Entities
{
    public class Subscription : Entity
    {
        public string SubscriberId { get; private set; }
        public Subscriber Subscriber { get; private set; }

        public int CompanyId { get; private set; }
        public Company Company { get; private set; }

        public string? Note { get; private set; }

        private readonly List<Permission> _permissions = new();
        public IReadOnlyCollection<Permission> Permissions => _permissions.AsReadOnly();


        protected Subscription()
        {
            SubscriberId = null!;
            Subscriber = null!;
            Company = null!;
        }

        protected Subscription(string? note) : this()
        {
            Note = note;
        }

        public Subscription(string subscriberId, int companyId, string? note) : this(note)
        {
            SubscriberId = subscriberId;
            CompanyId = companyId;
        }

        public Subscription(Subscriber subscriber, Company company, string? note) : this(note)
        {
            Subscriber = subscriber;
            Company = company;
        }
    }
}
