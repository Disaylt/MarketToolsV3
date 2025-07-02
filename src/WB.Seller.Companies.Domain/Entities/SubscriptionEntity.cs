using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WB.Seller.Companies.Domain.Enums;
using WB.Seller.Companies.Domain.Seed;

namespace WB.Seller.Companies.Domain.Entities
{
    public class SubscriptionEntity : Entity
    {
        public string UserId { get; private set; }
        public UserEntity User { get; private set; }

        public int CompanyId { get; private set; }
        public CompanyEntity Company { get; private set; }

        public string? Note { get; private set; }
        public SubscriptionRole Role { get; private set; }

        private readonly List<PermissionEntity> _permissions = [];
        public IReadOnlyCollection<PermissionEntity> Permissions => _permissions;

        protected SubscriptionEntity()
        {
            UserId = null!;
            User = null!;
            Company = null!;
        }

        protected SubscriptionEntity(string? note, SubscriptionRole role) : this()
        {
            Note = note;
            Role = role;
        }

        public SubscriptionEntity(string userId, int companyId, string? note, SubscriptionRole role) : this(note, role)
        {
            UserId = userId;
            CompanyId = companyId;
        }

        public SubscriptionEntity(UserEntity user, CompanyEntity company, string? note, SubscriptionRole role) : this(note, role)
        {
            User = user;
            Company = company;
        }

        public void AddPermission(PermissionEntity permission)
        {
            _permissions.Add(permission);
        }
    }
}
