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

        protected Permission()
        {

        }

        public Permission(PermissionStatus status, PermissionType type) : this()
        {
            Status = status;
            Type = type;
        }
    }
}
