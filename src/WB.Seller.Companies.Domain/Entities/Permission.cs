using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WB.Seller.Companies.Domain.Enums;
using WB.Seller.Companies.Domain.Seed;

namespace WB.Seller.Companies.Domain.Entities
{
    public class Permission : Entity
    {
        public PermissionStatus Status { get; private set; }
        public PermissionType Type { get; private set; }

        public int SubscriptionId { get; private set; }
        public Subscription Subscription { get; private set; }


        protected Permission()
        {
            Subscription = null!;
        }

        public Permission(PermissionStatus status, PermissionType type, int subscriptionId) : this()
        {
            Status = status;
            Type = type;
            SubscriptionId = subscriptionId;
        }
    }
}
