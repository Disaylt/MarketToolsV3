using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using WB.Seller.Companies.Domain.Entities;
using WB.Seller.Companies.Domain.Seed;

namespace WB.Seller.Companies.Application.Commands
{
    public class AddUserCommandHandler(IRepository<UserEntity> userRepository)
        : IRequestHandler<AddUserCommand, Unit>
    {
        public async Task<Unit> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            UserEntity user = new UserEntity(request.UserId, request.Login);
            await userRepository.AddAsync(user, cancellationToken);
            await userRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
