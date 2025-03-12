using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using WB.Seller.Companies.Application.Models;
using WB.Seller.Companies.Domain.Entities;
using WB.Seller.Companies.Domain.Enums;
using WB.Seller.Companies.Domain.Seed;

namespace WB.Seller.Companies.Application.Commands
{
    public class CreateCompanyCommandHandler(IRepository<CompanyEntity> companyRepository,
        IMapperFactory mapperFactory,
        IRepository<UserEntity> userRepository,
        IRepository<SubscriptionEntity> subscriptionRepository)
        : IRequestHandler<CreateCompanyCommand, CompanyDto>
    {
        public async Task<CompanyDto> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
        {
            var owner = await userRepository.FindByIdRequiredAsync(request.UserId, cancellationToken);

            CompanyEntity newCompany = new(request.Name, request.Token);
            await companyRepository.AddAsync(newCompany,cancellationToken);

            SubscriptionEntity newSubscription = new(owner, newCompany, null, SubscriptionRole.Owner);
            await subscriptionRepository.AddAsync(newSubscription,cancellationToken);

            await companyRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return mapperFactory
                .CreateFromMapper<CompanyEntity, CompanyDto>()
                .Map(newCompany);
        }
    }
}
