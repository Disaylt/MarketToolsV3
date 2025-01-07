using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WB.Seller.Api.Companies.Domain.Seed;

namespace WB.Seller.Api.Companies.Domain.Entities
{
    public class Owner : Entity
    {
        private const string PropertyErrorMessage = "Owner does not use this property";
        public string UserId { get; private set; } = null!;

        private readonly List<Company> _companies;
        public IReadOnlyCollection<Company> Companies => _companies.AsReadOnly();

        public override int Id
        {
            get => throw new NullReferenceException(PropertyErrorMessage);
            protected set => throw new NullReferenceException(PropertyErrorMessage);
        }

        protected Owner()
        {
            _companies = new();
        }

        public Owner(string userId) : this()
        {
            UserId = userId;
        }
    }
}
