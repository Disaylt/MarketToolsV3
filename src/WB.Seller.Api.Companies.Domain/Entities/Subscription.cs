using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WB.Seller.Api.Companies.Domain.Entities
{
    public class Subscription
    {
        public string SubscriberId { get; private set; }
        public Subscriber Subscriber { get; private set; }

        public int CompanyId { get; private set; }
        public Company Company { get; private set; }

        protected Subscription()
        {
            SubscriberId = null!;
            Subscriber = null!;
            Company = null!;
        }

        public Subscription(string subscriberId, int companyId) : this()
        {
            SubscriberId = subscriberId;
            CompanyId = companyId;
        }

        public Subscription(Subscriber subscriber, Company company) : this()
        {
            Subscriber = subscriber;
            Company = company;
        }
    }
}
