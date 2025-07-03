using WB.Seller.Companies.Application.Seed;
using WB.Seller.Companies.Application.Services.Abstract;
using WB.Seller.Companies.Domain.Entities;
using WB.Seller.Companies.Domain.Seed;

namespace WB.Seller.Companies.Infrastructure.Services.Implementation;

public class UserEntityService(IRepository<UserEntity> userRepository, IQueryHandleService queryHandleService)
    : IUserEntityService
{
    public Task<UserEntity?> FindUserByLoginAsync(string login)
    {
        var normalizedLogin = login.ToUpper();
        var query = userRepository
            .AsQueryable()
            .Where(x => x.NormalizedLogin == normalizedLogin);

        return queryHandleService.FirstOrDefaultAsync(query);
    }
}