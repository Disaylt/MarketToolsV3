using WB.Seller.Companies.Domain.Seed;

namespace WB.Seller.Companies.Domain.Entities
{
    public class Owner : Entity
    {
        private const string PropertyErrorMessage = "Owner does not use this property";
        public string UserId { get; private set; } = null!;
        public override int Id
        {
            get => throw new NullReferenceException(PropertyErrorMessage);
            protected set => throw new NullReferenceException(PropertyErrorMessage);
        }

        private readonly List<Company> _companies = [];
        public IReadOnlyCollection<Company> Companies => _companies.AsReadOnly();

        protected Owner()
        {

        }

        public Owner(string userId) : this()
        {
            UserId = userId;
        }
    }
}
