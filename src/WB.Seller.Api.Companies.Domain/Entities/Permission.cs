using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WB.Seller.Api.Companies.Domain.Enums;
using WB.Seller.Api.Companies.Domain.Seed;

namespace WB.Seller.Api.Companies.Domain.Entities
{
    //добавить в конфигурацию уникальность по типу и ид 
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
