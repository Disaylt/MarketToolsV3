using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WB.Seller.Companies.Domain.Enums;
using WB.Seller.Companies.Domain.Seed;

namespace WB.Seller.Companies.Domain.Entities
{
    public class PermissionEntity : Entity
    {
        public PermissionStatus Status { get; private set; }
        public string Type { get; private set; }

        public int SubscriptionId { get; private set; }
        public SubscriptionEntity Subscription { get; private set; }


        protected PermissionEntity()
        {
            Subscription = null!;
            Type = string.Empty;
        }

        public PermissionEntity(PermissionStatus status, string type, int subscriptionId) : this()
        {
            Status = status;
            Type = type;
            SubscriptionId = subscriptionId;
        }
    }
}
