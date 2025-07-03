using WB.Seller.Companies.Application.Services.Abstract;
using WB.Seller.Companies.Domain.Entities;

namespace WB.Seller.Companies.Infrastructure.Services.Implementation;

public class UserEntityService : IUserEntityService
{
    public Task<UserEntity?> FindUserByLoginAsync(string login)
    {
        throw new NotImplementedException();
    }
}